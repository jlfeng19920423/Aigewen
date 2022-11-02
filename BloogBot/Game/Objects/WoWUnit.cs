using BloogBot.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;


namespace BloogBot.Game.Objects
{
    public class WoWUnit : WoWObject
    {

        internal WoWUnit(IntPtr pointer, CGGuid guid, ObjectType objectType)
            : base(pointer, guid, objectType)
        {
        }
        
        /// <summary>
        /// property of spell
        /// </summary>
        public int CastID => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.CastID));
        public int ChanID => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.ChanID));
        public int CastStart => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.CastStart));
        public int ChanStart => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.ChanStart));
        public int CastEnd => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.CastEnd));
        public int ChanEnd => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.ChanEnd));


        public int Health => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.Health));
        public int MaxHealth => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.MaxHealth));
        public int Level => MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Unit.Level));
        
        public byte Sex => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.Sex));
        public byte Race => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.Race));
        public Byte Class => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.Class));

        public IntPtr InfoIntPtr => MemoryManager.ReadIntPtr(IntPtr.Add(EntPtr, Fields.Unit.Info));

        public IntPtr namePtr => MemoryManager.ReadIntPtr(IntPtr.Add(InfoIntPtr, Fields.Unit.Name));
        
        public string Name => MemoryManager.ReadStringName(namePtr, Encoding.UTF8);

        
        //Unit's Target

        public CGGuid TargetGuid => MemoryManager.ReadGuid(IntPtr.Add(EntPtr, Fields.Unit.Target));
        public IntPtr TargetGuidPtr => IntPtr.Add(EntPtr, Fields.Unit.Target);
        /// <summary>
        /// property of movement
        /// </summary>
        public IntPtr MovePtr => MemoryManager.ReadIntPtr(IntPtr.Add(EntPtr, Fields.Unit.Movement));   //correct
        public Position UnitPosition => new Position(
            MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location)),
            MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 4)),
            MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 8)));
        public float RotationD => MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.RotationD));
        public float RotationF => MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.RotationF));
        public float Pitch => MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Pitch));
        public byte MoveFlag => MemoryManager.ReadByte(IntPtr.Add(MovePtr, Fields.Unit.MoveFlag));

        /// <summary>
        /// Flags
        /// </summary>
        public UInt32 UnitFlag1 => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.UnitFlag1));
        public UInt32 UnitFlag2 => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.UnitFlag2));
        public UInt32 UnitFlag3 => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.UnitFlag3));
        public UInt32 DynamicFlag => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Fields.Unit.DynamicFlag));

        public bool IsDead => (this.Health <= 0 || (this.DynamicFlag == (UInt32)UnitDynFlags.UNIT_DYNFLAG_LOOTABLE)) ? true : false;
        public bool IsLootable => (this.DynamicFlag == (UInt32)UnitDynFlags.UNIT_DYNFLAG_LOOTABLE) ? true : false;
        public bool IsInCombat => (this.UnitFlag1 == (UInt32)UnitFlags.UNIT_FLAG_IN_COMBAT) ? true : false;
        public bool IsSkinnable => (this.UnitFlag1 == (UInt32)UnitFlags.UNIT_FLAG_SKINNABLE) ? true : false;

        public uint AuraCount => (uint)MemoryManager.ReadInt(IntPtr.Add(EntPtr, Fields.Aura.Table1));

        /*
        public IList<uint> AuraList()
        {
            IList<uint> mBuffs = new List<uint>();

            for (int currentAuraCount = 0; currentAuraCount < this.AuraCount; currentAuraCount++)
            {
                uint Table;
                int offset = (Fields.Aura.Table2 + currentAuraCount * Fields.Aura.Size) + 0x88;
                Table = MemoryManager.ReadUint(IntPtr.Add(this.EntPtr, offset));
                if (Table > 0)
                {
                    mBuffs.Add(Table);
                }
            }
            return mBuffs;
        }
        public IEnumerable<uint> AllBuffs => this.AuraList();
        */
        public bool HasAura(int AuraId)
        {
            int Buff;
            for (int currentAuraCount = 0; currentAuraCount < this.AuraCount; currentAuraCount++)
            {
                int offset = (Fields.Aura.Table2 + currentAuraCount * Fields.Aura.Size) + 0x88;
                Buff = MemoryManager.ReadInt(IntPtr.Add(this.EntPtr, offset));
                if (Buff == AuraId)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
