namespace Rocket.Libraries.FormValidationHelper.Shared.CharacterWhitelisting
{
    using System;

    interface ITerminalWhiteListCharacterFetcher : IDisposable
    {
        char Get(CharacterRangeDescription range);
    }
}