using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BloogBot.Game.Enums;

namespace BloogBot.Game
{
    public class TraverseObjMgr
    {
        public const int array = 0x8;
        public const int objGuid = 0x8;
        public const int entGuid = 0x18;
        public const int objType = 0x10;

        public static IntPtr CurMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
        public static Int32 Count = MemoryManager.ReadInt(CurMgr);
        public static IntPtr ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(CurMgr, array));

        public static IntPtr GetObjectEntPtr(CGGuid guid)
        {           
            IntPtr retPtr = IntPtr.Zero;
            
            for (int i = 0; i < Count; i++)
            {
                var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * array));
                if (ptr == IntPtr.Zero) continue;
                while (ptr != IntPtr.Zero)
                {
                    var entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, entGuid));
                    var guid1 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid));
                    var guid2 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid) + 0x8);
                    
                    if (guid1 == guid.low && guid2 == guid.high)
                    {
                        retPtr = entryPtr;
                        return retPtr;
                    }
                    
                    ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x0));
                }
            }
            
            if(retPtr == IntPtr.Zero) return retPtr;
            
            return retPtr;
        }
    }
}
