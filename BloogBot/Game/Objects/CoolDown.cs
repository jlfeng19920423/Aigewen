using BloogBot.Game.Enums;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;


namespace BloogBot.Game.Objects
{
    public class CoolDown
    {
        public readonly int SpellId;
        public readonly int ItemId;
        public readonly int StartTime;
        public readonly int GCDStartTime;


        public CoolDown(int spellid, int itemid, int startTime, int gcdStartTime)
        {
            SpellId = spellid;
            ItemId = itemid;
            StartTime = startTime;
            GCDStartTime = gcdStartTime;
        }
    }
}
