using BloogBot.Game.Enums;
using BloogBot.Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Diagnostics;


namespace BloogBot.Game
{
    public class ObjectManager
    {
        static CGGuid playerGuid;

        internal static IList<WoWObject> Objects = new List<WoWObject>();
        static internal IList<WoWObject> ObjectsBuffer = new List<WoWObject>();

        static public LocalPlayer Player { get; private set; }

        static public LocalPet Pet { get; private set; }

        static public IEnumerable<WoWObject> AllObjects => Objects;

        static public IEnumerable<WoWUnit> Units => Objects.OfType<WoWUnit>().Where(o => o.ObjectType == ObjectType.Unit).ToList();

        static public IEnumerable<WoWPlayer> Players => Objects.OfType<WoWPlayer>();

        static public IEnumerable<WoWItem> Items => Objects.OfType<WoWItem>();

        static public IEnumerable<WoWGameObject> GameObjects => Objects.OfType<WoWGameObject>();

        public static bool IsLoggedIn => MemoryManager.ReadByte(MemoryAddresses.MemBase + Offsets.InWorld) > 0;

        public static void GetTargetGuid(CGGuid targetGuid)
        {
            targetGuid.low = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid));
            targetGuid.high = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid) + 0x8);
        }

        static internal async void StartTraverseObjMgr()
        {
            while (true)
            {
                try
                {
                    TraverseObjectManager();
                    await Task.Delay(200);
                }
                catch (Exception e)
                {
                    //Logger.Log(e);
                }
            }
        }

        public static IntPtr CurMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
        public static Int32 Count = MemoryManager.ReadInt(CurMgr);
        public static IntPtr ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(CurMgr, Offsets.array));
        internal static void TraverseObjectManager()
        {
            if (IsLoggedIn)
            {
                CGGuid guid;
                IntPtr entryPtr;
                playerGuid.low = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid));
                playerGuid.high = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid) + 0x8);
                
                // here we traverse objects
                for (int i = 0; i < Count; i++)
                {
                    var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * Offsets.array));
                    if (ptr == IntPtr.Zero) continue;
                    while (ptr != IntPtr.Zero)
                    {
                        entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, Offsets.entptr));
                        guid.low = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, Offsets.objGuid));
                        guid.high = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, Offsets.objGuid) + 0x8);
                        var objectType = (ObjectType)MemoryManager.ReadByte(entryPtr + Offsets.objType);

                        try
                        {
                            switch (objectType)
                            {
                                case ObjectType.Container:
                                case ObjectType.Item:
                                    ObjectsBuffer.Add(new WoWItem(ptr, guid, objectType));
                                    break;
                                case ObjectType.Player:
                                    ObjectsBuffer.Add(new WoWPlayer(ptr, guid, objectType));
                                    break;
                                case ObjectType.LocalPlayer:
                                    var player = new LocalPlayer(ptr, guid, objectType);
                                    Player = player;
                                    ObjectsBuffer.Add(player);
                                    break;
                                case ObjectType.GameObject:
                                    ObjectsBuffer.Add(new WoWGameObject(ptr, guid, objectType));
                                    break;
                                case ObjectType.Unit:
                                    ObjectsBuffer.Add(new WoWUnit(ptr, guid, objectType));
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            //Logger.Log(e);
                        }

                        ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x0));
                    }
                }

                Objects = new List<WoWObject>(ObjectsBuffer);

                if (Player != null)
                {
                    var petFound = false;

                    foreach (var unit in Units)
                    {
                        //if (unit.SummonedByGuid == Player?.Guid)
                        //{
                        //    Pet = new LocalPet(unit.Pointer, unit.Guid, unit.ObjectType);
                        //    petFound = true;
                        //}

                        if (!petFound)
                            Pet = null;
                    }

                    // TODO
                    //Player.RefreshSpells();
                    //UpdateProbe();
                }
            }
        }

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        static void SetHardwareEvent()
        {
            IntPtr hardwareEventPtr = IntPtr.Add(Offsets.MemBase, Offsets.EvenTime);

            long perfCount;
            long freq;

            QueryPerformanceFrequency(out freq);
            QueryPerformanceCounter(out perfCount);

            long currentTime = (perfCount * 1000) / freq;

            var customBytes = BitConverter.GetBytes(currentTime);
            MemoryManager.WriteBytes(hardwareEventPtr, customBytes);
        }
    }
}