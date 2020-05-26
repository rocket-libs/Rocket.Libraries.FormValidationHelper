namespace Rocket.Libraries.FormValidationHelper.Shared.CharacterWhitelisting
{
    sealed class TerminalWhiteListCharacterFetcher : ITerminalWhiteListCharacterFetcher
    {
        public void Dispose()
        {
        }

        public char Get(CharacterRangeDescription range)
        {
            var terminalChar = range.StartCharacter + range.RangeLength - 1;
            return (char)terminalChar;
        }
    }
}