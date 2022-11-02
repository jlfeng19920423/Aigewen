using BloogBot.Game.Enums;
using System.Text;
using System;

namespace BloogBot.Game.Objects
{
    public class WoWGameObject : WoWObject
    {
        internal WoWGameObject(
            IntPtr pointer,
            CGGuid guid,
            ObjectType objectType)
            : base(pointer, guid, objectType)
        {
        }

        public byte Animating => MemoryManager.ReadByte(IntPtr.Add(EntPtr, Offsets_3_4_0_45506.GameObjectFishingBobberFlag));

        public CGGuid Creator => MemoryManager.ReadGuid(IntPtr.Add(EntPtr, Offsets_3_4_0_45506.GameObjectCreator));

        public IntPtr InfoPtr => MemoryManager.ReadIntPtr(IntPtr.Add(EntPtr, Offsets_3_4_0_45506.GameObjectNamePointer));
        public IntPtr NamePtr => MemoryManager.ReadIntPtr(IntPtr.Add(InfoPtr, Offsets_3_4_0_45506.GameObjectName));
        public string Name => MemoryManager.ReadStringName(NamePtr, Encoding.UTF8);
    }
}