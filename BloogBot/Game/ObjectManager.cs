using BloogBot.Game.Enums;
using BloogBot.Game.Objects;
using System;
using System.Text;
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

        internal static Dictionary<CGGuid, string> PlayerNames = new Dictionary<CGGuid, string>();

        static public LocalPlayer Player { get; private set; }

        static public LocalPet Pet { get; private set; }

        static public IEnumerable<WoWObject> AllObjects => Objects;

        static public IEnumerable<WoWUnit> Units => Objects.OfType<WoWUnit>().Where(o => o.ObjectType == ObjectType.Unit).ToList();

        static public IEnumerable<WoWPlayer> Players => Objects.OfType<WoWPlayer>();

        static public IEnumerable<WoWItem> Items => Objects.OfType<WoWItem>();

        static public IEnumerable<WoWGameObject> GameObjects => Objects.OfType<WoWGameObject>();

        public static bool IsLoggedIn => MemoryManager.ReadByte(MemoryAddresses.MemBase + Offsets.InWorld) > 0;

        static internal async void StartTraverseObjMgr()
        {
            while (true)
            {
                try
                {
                    TraverseNameManager();
                    TraverseObjectManager();
                    await Task.Delay(200);
                }
                catch (Exception e)
                {
                    //Logger.Log(e);
                }
            }
        }

        
        internal static void TraverseObjectManager()
        {
            if (IsLoggedIn)
            {
                IntPtr ObjMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
                int Count = MemoryManager.ReadInt(ObjMgr);
                IntPtr ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(ObjMgr, Offsets.array));
                CGGuid guid;
                IntPtr entryPtr;
                playerGuid = MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid));
                
                // here we traverse objects
                for (int i = 0; i < Count; i++)
                {
                    var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * Offsets.array));
                    if (ptr == IntPtr.Zero) continue;
                    while (ptr != IntPtr.Zero)
                    {
                        entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, Offsets.entptr));
                        guid = MemoryManager.ReadGuid(IntPtr.Add(entryPtr, Offsets.objGuid));
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

        internal static void TraverseNameManager()
        {
            if (IsLoggedIn)
            {
                try
                {
                    CGGuid guid;
                    IntPtr NameEntry;
                    string Name;

                    var NameMgr = IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Names);
                    var NameCount = MemoryManager.ReadInt(IntPtr.Add(NameMgr, Offsets.nameCount));
                    var NameArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(NameMgr, Offsets.nameArrayAddr));
                    if (NameCount <= 0) return;
                    // here we traverse objects
                    for (int i = 0; i < NameCount; i++)
                    {
                        NameEntry = MemoryManager.ReadIntPtr(IntPtr.Add(NameArrayAddr, i * Offsets.nameArray));
                        if (NameEntry == IntPtr.Zero) continue;
                        do
                        {
                            guid = MemoryManager.ReadGuid(IntPtr.Add(NameEntry, Offsets.nameGuid));
                            Name = MemoryManager.ReadStringName(IntPtr.Add(NameEntry, Offsets.nameName), Encoding.UTF8);
                            if (guid.isEmpty() || string.IsNullOrEmpty(Name)) continue;
                            if (!PlayerNames.ContainsKey(guid))
                                PlayerNames.Add(guid, Name);

                            NameEntry = MemoryManager.ReadIntPtr(NameEntry + 0x0);
                        } while (NameEntry != IntPtr.Zero);
                    }
                }
                catch (Exception e)
                {
                    //return false;
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