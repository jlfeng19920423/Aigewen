using BloogBot.Game.Enums;
using System;

namespace BloogBot.Game.Objects
{
    public class LocalPet : WoWUnit
    {
        internal LocalPet(
            IntPtr pointer,
            CGGuid guid,
            ObjectType objectType)
            : base(pointer, guid, objectType)
        {
        }
    }
}
