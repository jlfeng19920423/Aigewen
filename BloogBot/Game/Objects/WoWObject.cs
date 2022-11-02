using BloogBot.Game.Enums;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;


namespace BloogBot.Game.Objects
{
    public unsafe abstract class WoWObject
    {
        public readonly IntPtr Pointer;
        public readonly CGGuid Guid;
        public readonly ObjectType ObjectType;
        public readonly IntPtr EntPtr;
        public readonly IntPtr GuidPtr;

        internal WoWObject(IntPtr pointer, CGGuid guid, ObjectType objectType)
        {
            Guid = guid;
            Pointer = pointer;
            ObjectType = objectType;
            EntPtr = MemoryManager.ReadIntPtr(IntPtr.Add(pointer, Offsets.entptr));
            GuidPtr = IntPtr.Add(EntPtr, Offsets.objGuid);
        }
    }
}
