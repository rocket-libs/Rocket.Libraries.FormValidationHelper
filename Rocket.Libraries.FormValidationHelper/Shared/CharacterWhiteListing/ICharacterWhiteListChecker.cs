using System;

namespace Rocket.Libraries.FormValidationHelper.Shared.CharacterWhitelisting
{
    interface ICharacterWhiteListChecker : IDisposable
    {
        ICharacterWhiteListChecker AddRangeDescription(char startCharacter, ushort rangeLength);

        char GetFirstInvalidCharacter(string suspect);
    }
}