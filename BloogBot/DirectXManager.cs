using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BloogBot
{
    public static class DirectXManager
    {
        const int I_SCENE_END_FUN_PTR = 0x005A17A0;

        static int lastFrameTick;
        static int timeBetweenFrame;
        static int waitTilNextFrame;

        // endSceneDetour needs to be a class field (as opposed to local variable)
        // otherwise the GC will clean it up and crash the bot
        static Direct3D9EndScene endSceneDetour;
        static Direct3D9EndScene endSceneOriginal;
        static IntPtr endScenePtr;
        static Direct3D9ISceneEnd iSceneEndDelegate;
        static IntPtr target;
        static List<byte> original;

        // if frames are rendering faster than once every ~16ms (60fps), slow them down
        // this corrects an issue where ClickToMove doesn't work when your monitor has a refresh rate above ~80
        internal static void ThrottleFPS()
        {
            GetEndScenePtr();
            endSceneOriginal = Marshal.GetDelegateForFunctionPointer<Direct3D9EndScene>(MemoryManager.ReadIntPtr(endScenePtr));
            endSceneDetour = new Direct3D9EndScene(EndSceneHook);

            var addrToDetour = Marshal.GetFunctionPointerForDelegate(endSceneDetour);
            var customBytes = BitConverter.GetBytes((int)addrToDetour);
            MemoryManager.WriteBytes(endScenePtr, customBytes);
        }

        static int EndSceneHook(IntPtr device)
        {
            if (lastFrameTick != 0)
            {
                timeBetweenFrame = Environment.TickCount - lastFrameTick;
                if (timeBetweenFrame < 15)
                {
                    var newCount = Environment.TickCount;
                    waitTilNextFrame = 15 - timeBetweenFrame;
                    newCount += waitTilNextFrame;
                    while (Environment.TickCount < newCount) { }
                }
            }
            lastFrameTick = Environment.TickCount;

            return endSceneOriginal(device);
        }

        static void GetEndScenePtr()
        {
            iSceneEndDelegate = Marshal.GetDelegateForFunctionPointer<Direct3D9ISceneEnd>((IntPtr)I_SCENE_END_FUN_PTR);
            target = Marshal.GetFunctionPointerForDelegate(iSceneEndDelegate);
            var hook = Marshal.GetFunctionPointerForDelegate(new Direct3D9ISceneEnd(ISceneEndHook));

            // note the original bytes so we can unhook ISceneEnd after finding endScenePtr
            original = new List<byte>();
            original.AddRange(MemoryManager.ReadBytes(target, 6));

            // hook ISceneEnd
            var detour = new List<byte> { 0x68 }; // opcode for push instruction
            var bytes = BitConverter.GetBytes(hook.ToInt32());
            detour.AddRange(bytes);
            detour.Add(0xC3); // opcode for retn instruction
            MemoryManager.WriteBytes(target, detour.ToArray());

            // wait for ISceneEndHook to set endScenePtr
            while (endScenePtr == default(IntPtr))
                Task.Delay(3);
        }

        static IntPtr ISceneEndHook(IntPtr ptr)
        {
            var ptr1 = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x38A8));
            var ptr2 = MemoryManager.ReadIntPtr(ptr1);
            endScenePtr = IntPtr.Add(ptr2, 0xa8);

            // unhook ISceneEnd
            MemoryManager.WriteBytes(target, original.ToArray());

            return iSceneEndDelegate(ptr);
        }

        delegate int Direct3D9EndScene(IntPtr device);

        [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
        delegate IntPtr Direct3D9ISceneEnd(IntPtr unk);
    }
}
