using BloogBot.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloogBot.Game.Objects
{
    public class LocalPlayer : WoWPlayer
    {
        internal LocalPlayer(IntPtr pointer, CGGuid guid, ObjectType objectType)
            : base(pointer, guid, objectType)
        {
            //RefreshSpells();
        }

        readonly IDictionary<string, int[]> playerSpells = new Dictionary<string, int[]>();


        //public void Jump() => Functions.Jump();
        public Position CorpsePosition => new Position(
            MemoryManager.ReadFloat(IntPtr.Add(MemoryAddresses.MemBase, Offsets.CorpseCorpseMapId) + Offsets.CorpsePositionOffset),
            MemoryManager.ReadFloat(IntPtr.Add(MemoryAddresses.MemBase, Offsets.CorpseCorpseMapId) + Offsets.CorpsePositionOffset + 4),
            MemoryManager.ReadFloat(IntPtr.Add(MemoryAddresses.MemBase, Offsets.CorpseCorpseMapId) + Offsets.CorpsePositionOffset + 8));
        
        public IntPtr LastTargetGuidPtr => IntPtr.Add(MemoryAddresses.MemBase, Offsets.LastTargetGuid);
        public CGGuid LastTargetGuid => MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.LastTargetGuid));


        public bool IsGhost => this.Health == 1;

        public void ClearAfk()
        {
            ThreadSynchronizer.RunOnMainThread(() => Functions.ClearAFK(this.EntPtr));
        }

        public void RetrieveCorpse()
        {
            ThreadSynchronizer.RunOnMainThread(() => Functions.RetrieveCorpse());
        }

        public bool KnowsSpell(int spellId, bool isPet)
        {
            int ret = ThreadSynchronizer.RunOnMainThread(() => Functions.FindSlotBySpellId(spellId, isPet));
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
