using BloogBot.Game.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BloogBot.Game.Objects
{
    public class WoWPlayer : WoWUnit
    {
        readonly Dictionary<CGGuid, string> playerNames;
        internal WoWPlayer(IntPtr pointer, CGGuid guid, ObjectType objectType)
            : base(pointer, guid, objectType)
        {
            this.playerNames = ObjectManager.PlayerNames;
        }

        public new string Name => playerNames.FirstOrDefault(u => u.Key.isEqualTo(Guid)).Value;

    }
}
