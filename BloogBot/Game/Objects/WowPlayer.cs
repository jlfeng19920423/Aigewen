using BloogBot.Game.Enums;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BloogBot.Game.Objects
{
    public class WoWPlayer : WoWUnit
    {
        
        internal WoWPlayer(IntPtr pointer, CGGuid guid, ObjectType objectType)
            : base(pointer, guid, objectType)
        {
            
        }
        public Dictionary<CGGuid, string> playerNames => ObjectManager.PlayerNames;
        public new string Name => playerNames.FirstOrDefault(u => u.Key.isEqualTo(Guid)).Value;

    }
}
