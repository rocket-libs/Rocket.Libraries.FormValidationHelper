namespace Rocket.Libraries.FormValidationHelper.Shared.CharacterWhitelisting
{
    using System.Collections.Immutable;
    using System.Linq;

    sealed class CharacterWhiteListChecker : ICharacterWhiteListChecker
    {
        private readonly ITerminalWhiteListCharacterFetcher terminalWhiteListCharacterFetcher;

        private ImmutableList<CharacterRangeDescription> _ranges = ImmutableList<CharacterRangeDescription>.Empty;

        public CharacterWhiteListChecker(ITerminalWhiteListCharacterFetcher terminalWhiteListCharacterFetcher)
        {
            this.terminalWhiteListCharacterFetcher = terminalWhiteListCharacterFetcher;
        }

        public ICharacterWhiteListChecker AddRangeDescription(char startCharacter, ushort rangeLength)
        {
            var characterRangeDescription = new CharacterRangeDescription
            {
                RangeLength = rangeLength,
                StartCharacter = startCharacter,
            };

            _ranges = _ranges.Add(characterRangeDescription);
            return this;
        }

        public void Dispose()
        {
        }

        public char GetFirstInvalidCharacter(string suspectString)
        {
            var verifiedChars = ImmutableList<char>.Empty;
            using (terminalWhiteListCharacterFetcher)
            {
                foreach (var currentRange in _ranges)
                {
                    foreach (var currentCharacter in suspectString)
                    {
                        var notYetVerified = verifiedChars.Any(a => a == currentCharacter) == false;
                        if (notYetVerified)
                        {
                            var isValid = IsInWhiteList(currentRange, currentCharacter);
                            if (isValid)
                            {
                                verifiedChars = verifiedChars.Add(currentCharacter);
                            }
                        }
                    }
                }
            }

            return GetNotWhitelistedCharacter(suspectString, verifiedChars);
        }

        private char GetNotWhitelistedCharacter(string suspectString, ImmutableList<char> verifiedChars)
        {
            foreach (var currentChar in suspectString)
            {
                var isNotInVerifiedList = verifiedChars.Any(a => a == currentChar) == false;
                if (isNotInVerifiedList)
                {
                    return currentChar;
                }
            }
            return default;
        }

        private bool IsInWhiteList(CharacterRangeDescription currentRange, char currentCharacter)
        {
            var isInsideLowerBoundary = currentCharacter >= currentRange.StartCharacter;
            var upperBoundaryCharacter = terminalWhiteListCharacterFetcher.Get(currentRange);
            var isInsideUpperBoundary = currentCharacter <= upperBoundaryCharacter;
            var isValid = isInsideLowerBoundary && isInsideUpperBoundary;
            return isValid;
        }
    }
}