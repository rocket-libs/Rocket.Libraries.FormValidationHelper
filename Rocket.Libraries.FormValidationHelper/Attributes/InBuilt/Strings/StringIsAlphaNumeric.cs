using Rocket.Libraries.FormValidationHelper.Shared.CharacterWhitelisting;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsAlphaNumeric : ValidatorAttributeBase
    {
        public StringIsAlphaNumeric(string displayLabel)
         : base(displayLabel)
        {
        }

        public override string ErrorMessage => $"Only alpha numeric values are allowed for {ValidatorAttributeBase.DisplayLabelPlaceholder}";

        public override bool ValidationFailed(object value)
        {
            if(value == null)
            {
                return false;
            }
            else
            {
                const ushort alphabetRange = 26;
                var valueAsString = value.ToString();
                var characterWhitelistChecker = new CharacterWhiteListChecker(new TerminalWhiteListCharacterFetcher());
                characterWhitelistChecker
                    .AddRangeDescription('a', alphabetRange)
                    .AddRangeDescription('A', alphabetRange)
                    .AddRangeDescription('0', 10);
                var invalidChar = characterWhitelistChecker.GetFirstInvalidCharacter(valueAsString);
                return invalidChar != default;
            }
        }
    }
}