using BloogBot.Game;
using BloogBot.Game.Enums;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using BloogBot.Game.Objects;
using BloogBot.AI;
using BloogBot.Bots;
using System.Threading.Tasks;

namespace BloogBot.UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly Probe probe;
        readonly BotSettings botSettings;

        public MainViewModel()
        {
            /*
            void callback()
            {
                UpdatePropertiesWithAttribute(typeof(ProbeFieldAttribute));
            }
            void killswitch()
            {
                Stop();
            }
            probe = new Probe(callback, killswitch);
            */
            
            TravelPathGenerator.Initialize(() =>
            {
                OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
            });
            //InitializeTravelPaths();
        }

        public ObservableCollection<TravelPath> TravelPaths { get; private set; }
        public ObservableCollection<Hotspot> Hotspots { get; private set; }

        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<string> ConsoleOutput { get; } = new ObservableCollection<string>();

        void Log(string message)
        {
            ConsoleOutput.Add($"({DateTime.Now.ToShortTimeString()}) {message}");
            OnPropertyChanged(nameof(ConsoleOutput));
        }

        public void InitializeObjectManager()
        {
            //ObjectManager.StartTraverseObjMgr();
        }

        void InitializeTravelPaths()
        {
            TravelPaths = new ObservableCollection<TravelPath>(Repository.ListTravelPaths());
            TravelPaths.Insert(0, null);
            OnPropertyChanged(nameof(CurrentTravelPath));
            OnPropertyChanged(nameof(TravelPaths));
        }

        void UpdatePropertiesWithAttribute(Type type)
        {
            foreach (var propertyInfo in GetType().GetProperties())
            {
                if (Attribute.IsDefined(propertyInfo, type))
                    OnPropertyChanged(propertyInfo.Name);
            }
        }

        ICommand spellCommand;
        ICommand objectsCommand;
        ICommand getTargetEntCommand;
        ICommand ctmTargetCommand;
        ICommand funcTestCommand;
        ICommand clearLogCommand;

        public const int array = 0x8;
        public const int objGuid = 0x8;
        public const int entGuid = 0x18;
        public const int objType = 0x10;

        public int SpellSlot;
        public int spellid;
        public int SpellId
        {
            get => spellid;
            set
            {
                spellid = value;
                OnPropertyChanged(nameof(SpellId));
            }
        }

        
        unsafe void Spell()
        {
            ObjectManager.TraverseObjectManager();
            LocalPlayer player = ObjectManager.Player;
            WoWUnit Target = ObjectManager.Units.FirstOrDefault(u => u.Guid.isEqualTo(player.TargetGuid));
            if (Target != null)
            {
                Position destination = Target.UnitPosition;
                //TypedReference tr = __makeref(destination);
                //IntPtr ptr = **(IntPtr**)(&tr);
                float[] pos = { destination.X, destination.Y, destination.Z };
                IntPtr posPtr = MemoryManager.GetPosAddr(ref pos);
                /*
                fixed (float* ptr = &pos[0])
                {
                    IntPtr posPtr = (IntPtr)ptr;
                    ThreadSynchronizer.RunOnMainThread(() => Functions.ClickToMoveMoveTo(player.EntPtr, posPtr));
                }
                */
                ThreadSynchronizer.RunOnMainThread(() => Functions.ClickToMoveMoveTo(player.EntPtr, posPtr));
                //Log($"destination:{destination.X},{destination.Y},{destination.Z}\tpos:{posPtr.ToString("X2")}");
                //if (posPtr != IntPtr.Zero) ThreadSynchronizer.RunOnMainThread(() => Functions.ClickToMoveMoveTo(player.EntPtr, &pos[0]));
            }
            
            /*
            OnPropertyChanged(nameof(spellid));
            SpellSlot = ThreadSynchronizer.RunOnMainThread(() => Functions.FindSlotBySpellId(spellid,false));

            if (player.HasAura(spellid)==false)
            {
                var re = ThreadSynchronizer.RunOnMainThread(() => Functions.CastSpellBySlot(SpellSlot, player.LastTargetGuidPtr));

            }
            Log($"SpellSlot:{SpellSlot}");
            */
            //ThreadSynchronizer.RunOnMainThread(() => Functions.ReleaseCorpse(player.EntPtr));
            //ThreadSynchronizer.RunOnMainThread(() => Functions.AcceptResurrect());
        }
   

        IntPtr playerPtr = IntPtr.Zero;
        IntPtr pUnit = IntPtr.Zero;
        void CTMTarget()
        {
            ObjectManager.TraverseObjectManager();
            pUnit = IntPtr.Zero;
            var CurMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
            var Count = MemoryManager.ReadInt(CurMgr);
            var ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(CurMgr, array));
            var PlayerGuid1 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid));
            var PlayerGuid2 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid) + 0x8);
            var TargetGuid1 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid));
            var TargetGuid2 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid) + 0x8);
            var FocusGuid = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Focus_Guid));
            var LastTargetGuid = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Last_Target_Guid));
            Log($"测试：PlayerGuid:{PlayerGuid2.ToString("X2")}{PlayerGuid1.ToString("X2")}\t");
            for (int i = 0; i < Count; i++)
            {
                var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * array));
                if (ptr == IntPtr.Zero) continue;
                int n = 0;
                while (ptr != IntPtr.Zero)
                {
                    var entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, entGuid));
                    var guid1 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid));
                    var guid2 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid) + 0x8);
                    var type = (ObjectType)MemoryManager.ReadByte(entryPtr + objType);
                    /* LocalPlayer */
                    n += 1;
                    if (type == ObjectType.LocalPlayer)
                    {
                        //float UnitX = MemoryManager.ReadFloat(IntPtr.Add(entryPtr, Fields.LocalPlayer.Location));       //correct
                        //float UnitY = MemoryManager.ReadFloat(IntPtr.Add(entryPtr, Fields.LocalPlayer.Location + 4));     //correct
                        //float UnitZ = MemoryManager.ReadFloat(IntPtr.Add(entryPtr, Fields.LocalPlayer.Location + 8));     //correct
                        //Log($"i:{i}\t n:{n}\t type:{type}\t ptr: 0x{ptr.ToString("X2")} \t Entry:0x{entryPtr.ToString("X2")}\t Guid:{guid2}{guid1}\t Position:{UnitX}\t{UnitY}\t{UnitZ}");
                        LocalPlayerGuid1 = guid1;
                        LocalPlayerGuid2 = guid2;
                        playerPtr = entryPtr;
                    }

                    /* target properties */

                    if (guid1 == TargetGuid1 && guid2 == TargetGuid2 && type == ObjectType.Unit)//info for Unit
                    {
                        var level = MemoryManager.ReadInt(IntPtr.Add(entryPtr, Fields.Unit.Level));  //correct
                        var health = MemoryManager.ReadInt(IntPtr.Add(entryPtr, Fields.Unit.Health)); //correct
                        var InfoIntPtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, Fields.Unit.Info));
                        var namePtr = MemoryManager.ReadIntPtr(IntPtr.Add(InfoIntPtr, Fields.Unit.Name));
                        var nameUnit = MemoryManager.ReadStringName(namePtr, Encoding.UTF8);
                        var MovePtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, Fields.Unit.Movement));   //correct
                        float UnitX = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location));       //correct
                        float UnitY = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 4));     //correct
                        float UnitZ = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 8));     //correct
                        pUnit = IntPtr.Add(MovePtr, Fields.Unit.Location);
                        Log($"i:{i}\t n:{n}\t type:{type}\t ptr:0x{ptr.ToString("X2")} \t Entry:0x{entryPtr.ToString("X2")} \t TargetGuid:{guid2.ToString("X2")}{guid1.ToString("X2")}\t type:{type}\tlevel:{level}\t Health:{health}\t name:{nameUnit}\t X:{UnitX}\t Y:{UnitY}\t Z:{UnitZ}");
                    }


                    ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x0));
                }
            }
            if(pUnit!=IntPtr.Zero) ThreadSynchronizer.RunOnMainThread(() => Functions.ClickToMoveMoveTo(playerPtr, pUnit));
        }

        public int fishingId = 7620;

        ulong LocalPlayerGuid1 = 0;
        ulong LocalPlayerGuid2 = 0;

        byte Animating = 0;
        bool fishing;

        async void Fishing(WoWGameObject gameObject, LocalPlayer player)
        {
            while (fishing)
            {
                try
                {
                    if (player.ChanID == SpellId)
                    {
                        fishing = true;
                    }
                    else
                    {
                        fishing = false;
                        Log($"stop fishing");
                        return;
                    }
                    Animating = gameObject.Animating;
                    if (Animating!=0)
                    {
                        Log($"should interact");
                        var ret = ThreadSynchronizer.RunOnMainThread(() => Functions.GameUIOnSpriteRightClick(gameObject.GuidPtr));
                        fishing = false;
                        Log($"stop fishing");
                        return;
                    }
                    await Task.Delay(100);
                }
                catch (Exception e)
                {
                    //Logger.Log(e + "\n");
                }
            }
        }
        void FuncTest()
        {
            ObjectManager.TraverseObjectManager();
            LocalPlayer player = ObjectManager.Player;
            
            ThreadSynchronizer.RunOnMainThread(() => Functions.FaceTo(player.EntPtr,(float)1.6));

        }

        IntPtr BobberEntryPtr = IntPtr.Zero;
        
        byte ret = 0;
        void GetTargetEnt()
        {
            CGGuid lastTargetGuid;
            lastTargetGuid = MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.LastTargetGuid));
            LocalPlayer player = ObjectManager.Player;
            WoWUnit Target = ObjectManager.Units.FirstOrDefault(u => u.Guid.isEqualTo(player.TargetGuid));
            WoWPlayer Players = ObjectManager.Players.FirstOrDefault(u => u.Guid.isEqualTo(player.TargetGuid));
            WoWUnit LastTarget = ObjectManager.Units.FirstOrDefault(u => u.Guid.isEqualTo(player.LastTargetGuid));
            Log($"GameBase:0x{MemoryAddresses.MemBase.ToString("X2")}\t PlayerGuid:{player.Guid.high}{player.Guid.low}");
            Log($"CorpsePosition:({player.CorpsePosition.X},{player.CorpsePosition.Y},{player.CorpsePosition.Z})");
            if (Target != null)
            {
                Log($"TargetEntPtr:0x{Target.EntPtr.ToString("X2")}\tPlayerEntPtr:0x{player.EntPtr.ToString("X2")}\tTargetGuid:{Target.Guid.high}{Target.Guid.low}");
                //Log($"Health:{Target.Health}\tMaxHealth:{Target.MaxHealth}\tLevel:{Target.Level}\t");
                //Log($"Name:{Target.Name}\tSex:{Target.Sex}\tRace:{Target.Race}\tTargetGuid:{Target.TargetGuid.high}{Target.TargetGuid.low}");
                //Log($"Position:{Target.UnitPosition.X},{Target.UnitPosition.Y},{Target.UnitPosition.Z}\tRotationD:{Target.RotationD}\tRotationF:{Target.RotationF}\tPitch:{Target.Pitch}");
                //Log($"Flag1:{Target.UnitFlag1}\tFlag2:{Target.UnitFlag2}\tFlag3:{Target.UnitFlag3}\tDynamicFlag:{Target.DynamicFlag}");
            }
            if (Players != null)
            {
                Log($"PlayersEntPtr:0x{Players.EntPtr.ToString("X2")}\tPlayersGuid:{Players.Guid.high}{Players.Guid.low}");
                Log($"ChanId:{Players.ChanID}\t");
                //Log($"Name:{Players.Name}\tSex:{Players.Sex}\tRace:{Players.Race}\tTargetGuid:{Players.TargetGuid.high}{Players.TargetGuid.low}");
                //Log($"Position:{Players.UnitPosition.X},{Players.UnitPosition.Y},{Players.UnitPosition.Z}\tRotationD:{Players.RotationD}\tRotationF:{Players.RotationF}\tPitch:{Players.Pitch}");
                //Log($"Flag1:{Players.UnitFlag1}\tFlag2:{Players.UnitFlag2}\tFlag3:{Players.UnitFlag3}\tDynamicFlag:{Players.DynamicFlag}");
            }
            Log($"LastTargetGuid:{lastTargetGuid.high}{lastTargetGuid.low}");
        }

        
        void Objects()
        {
            var CurMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
            var Count = MemoryManager.ReadInt(CurMgr);
            var ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(CurMgr, array));
            var PlayerGuid1 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid));
            var PlayerGuid2 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid) + 0x8);
            var TargetGuid1 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid));
            var TargetGuid2 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid) + 0x8);
            var FocusGuid = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Focus_Guid));
            var LastTargetGuid = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Last_Target_Guid));
            Log($"测试：PlayerGuid:{PlayerGuid2.ToString("X2")}{PlayerGuid1.ToString("X2")}\t");
            for (int i = 0; i < Count; i++)
            {
                var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * array));
                if (ptr == IntPtr.Zero) continue;
                int n = 0;
                while (ptr != IntPtr.Zero)
                {
                    var entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, entGuid));
                    var guid1 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid));
                    var guid2 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid) + 0x8);
                    var type = (ObjectType)MemoryManager.ReadByte(entryPtr + objType);
                    /* LocalPlayer */
                    n += 1;
                    if (type ==ObjectType.LocalPlayer)
                    {
                        Log($"i:{i}\t n:{n}\t type:{type}\t ptr: 0x{ptr.ToString("X2")} \t Entry:0x{entryPtr.ToString("X2")}\t Guid:{guid2}{guid1}");
                        LocalPlayerGuid1 = guid1;
                        LocalPlayerGuid2 = guid2;
                    }
                    
                    /* target properties */
                    
                    if (guid1 == TargetGuid1 && guid2 == TargetGuid2 && type == ObjectType.Unit)//info for Unit
                    {
                        var level = MemoryManager.ReadInt(IntPtr.Add(entryPtr, Fields.Unit.Level));  //correct
                        var health = MemoryManager.ReadInt(IntPtr.Add(entryPtr, Fields.Unit.Health)); //correct
                        var InfoIntPtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, Fields.Unit.Info));
                        var namePtr = MemoryManager.ReadIntPtr(IntPtr.Add(InfoIntPtr, Fields.Unit.Name));
                        var nameUnit = MemoryManager.ReadStringName(namePtr, Encoding.UTF8);
                        var MovePtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, Fields.Unit.Movement));   //correct
                        var UnitX = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location));       //correct
                        var UnitY = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 4));     //correct
                        var UnitZ = MemoryManager.ReadFloat(IntPtr.Add(MovePtr, Fields.Unit.Location + 8));     //correct
                        Log($"i:{i}\t n:{n}\t type:{type}\t ptr:0x{ptr.ToString("X2")} \t Entry:0x{entryPtr.ToString("X2")} \t TargetGuid:{guid2.ToString("X2")}{guid1.ToString("X2")}\t type:{type}\tlevel:{level}\t Health:{health}\t name:{nameUnit}\t X:{UnitX}\t Y:{UnitY}\t Z:{UnitZ}");
                    }
                    
                    if (guid1 == TargetGuid1 && guid2 == TargetGuid2 && type == ObjectType.Player)//info for player
                    {
                        var namePtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, Fields.Object.Name1));   //correct
                        //var namePlayer = MemoryManager.ReadStringName(namePtr+Fields.Object.Name2, Encoding.UTF8);
                        Log($"i:{i}\t n:{n}\t type:{type}\t ptr:0x{ptr.ToString("X2")}\t Entry:0x{entryPtr.ToString("X2")} \t Guid:{guid2.ToString("X2")}{guid1.ToString("X2")}\t type:{type}");
                    }
                    
                    if (type == ObjectType.GameObject)
                    {
                        var ObjCreatorGuid1 = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, 0x210)); //correct
                        var ObjCreatorGuid2 = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, 0x210) + 0x8);   //correct
                        var AnimateState = MemoryManager.ReadByte(IntPtr.Add(entryPtr, 0xA0));  //correct
                        var namePtr = MemoryManager.ReadIntPtr(IntPtr.Add(entryPtr, 0x148));
                        var NamePtr = MemoryManager.ReadIntPtr(IntPtr.Add(namePtr, 0xE0));
                        var name = MemoryManager.ReadStringName(NamePtr, Encoding.UTF8);
                        //var ObjID = MemoryManager.ReadStringName(IntPtr.Add(entryPtr, Fields.Object.ObjectID), Encoding.UTF8);
                        Log($"i:{i}\t n:{n}\t type:{type}\t ptr:0x{ptr.ToString("X2")}\t Entry:0x{entryPtr.ToString("X2")}\t  CreatorGuid:{ObjCreatorGuid2}{ObjCreatorGuid1}\t AnimateState:{AnimateState}\t Name:{name}");
                    }
                    
                    ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x0));
                }
            }
            /*
            for (int i = 0; i < Count; i++)
            {
                var ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ArrayAddr, i * array));
                if (ptr == IntPtr.Zero) continue;
                int n = 0;
                while (ptr != IntPtr.Zero)
                {
                    var entryPtr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, entGuid));
                    var guid1 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid));
                    var guid2 = MemoryManager.ReadUlong(IntPtr.Add(ptr, objGuid) + 0x8);
                    var type = (ObjectType)MemoryManager.ReadByte(entryPtr + objType);
 
                    if (type == ObjectType.GameObject)
                    {
                        var ObjCreatorGuid1 = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, 0x210));
                        var ObjCreatorGuid2 = MemoryManager.ReadUlong(IntPtr.Add(entryPtr, 0x210) + 0x8);
                        var AnimateState = MemoryManager.ReadByte(IntPtr.Add(entryPtr, 0xA0));
                        //var ObjID = MemoryManager.ReadStringName(IntPtr.Add(entryPtr, Fields.Object.ObjectID), Encoding.UTF8);
                        //Log($"type:{type}\t Entry:0x{entryPtr.ToString("X2")}\t  CreatorGuid:{ObjCreatorGuid2}{ObjCreatorGuid1}\t AnimateState:{AnimateState}");

                        if (ObjCreatorGuid1 == LocalPlayerGuid1 && ObjCreatorGuid2 == LocalPlayerGuid2 && ObjCreatorGuid1 != 0 && ObjCreatorGuid2 != 0)
                        {
                            BobberEntryPtr = entryPtr;
                            Log($"type:{type}\t Entry:0x{entryPtr.ToString("X2")}\t  PlayerGuid:{ObjCreatorGuid2}{ObjCreatorGuid1}");
                        }
                    }
                    ptr = MemoryManager.ReadIntPtr(IntPtr.Add(ptr, 0x0));
                }
            }
            if (BobberEntryPtr != IntPtr.Zero)
            {
                do
                {
                    Animating = MemoryManager.ReadByte(IntPtr.Add(BobberEntryPtr, 0xA0));
                    if (Animating != 0)
                    {
                        SpellSlot = 8;
                        var re2 = ThreadSynchronizer.RunOnMainThread(() => Functions.CastSpellBySlot(SpellSlot, IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid)));
                        Log($"AnimateState:{Animating}");
                        break;
                    }
                    ret += 1;
                    Thread.Sleep(50);
                } while (ret < 400);
            }
            */
        }


        public ICommand SpellCommand =>
            spellCommand ?? (spellCommand = new CommandHandler(Spell, true));

        public ICommand ObjectsCommand =>
            objectsCommand ?? (objectsCommand = new CommandHandler(Objects, true));

        public ICommand CTMTargetCommand =>
            ctmTargetCommand ?? (ctmTargetCommand = new CommandHandler(CTMTarget, true));

        public ICommand GetTargetEntCommand =>
            getTargetEntCommand ?? (getTargetEntCommand = new CommandHandler(GetTargetEnt, true));
        public ICommand FuncTestCommand =>
            funcTestCommand ?? (funcTestCommand = new CommandHandler(FuncTest, true));

        IBot currentBot = new FishingBot();


        ActionList actionList = FishingBot.actionList;
        List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack = FishingBot.stateStack;

        public bool StartCommandEnabled => !currentBot.Running();

        public bool StopCommandEnabled => currentBot.Running();

        ICommand startCommand;

        void UiStart()
        {
            Start();
            Log("Bot started!");
        }

        void Start()
        {
            try
            {
                //ObjectManager.KillswitchTriggered = false;

                //var container = currentBot.GetDependencyContainer(botSettings, probe, Hotspots);

                void stopCallback()
                {
                    OnPropertyChanged(nameof(StartCommandEnabled));
                    OnPropertyChanged(nameof(StopCommandEnabled));
                    //OnPropertyChanged(nameof(StartPowerlevelCommandEnabled));
                    OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
                    //OnPropertyChanged(nameof(ReloadBotsCommandEnabled));
                    //OnPropertyChanged(nameof(CurrentBotEnabled));
                    //OnPropertyChanged(nameof(GrindingHotspotEnabled));
                    OnPropertyChanged(nameof(CurrentTravelPathEnabled));
                }

                currentBot.Start(stateStack, actionList, stopCallback);

                OnPropertyChanged(nameof(StartCommandEnabled));
                OnPropertyChanged(nameof(StopCommandEnabled));
                //OnPropertyChanged(nameof(StartPowerlevelCommandEnabled));
                OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
                //OnPropertyChanged(nameof(ReloadBotsCommandEnabled));
                //OnPropertyChanged(nameof(CurrentBotEnabled));
                //OnPropertyChanged(nameof(GrindingHotspotEnabled));
                OnPropertyChanged(nameof(CurrentTravelPathEnabled));
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand StartCommand =>
            startCommand ?? (startCommand = new CommandHandler(UiStart, true));

        // Stop command
        ICommand stopCommand;

        void UiStop()
        {
            Stop();
            Log("Bot stopped!");
        }

        void Stop()
        {
            try
            {
                //var container = currentBot.GetDependencyContainer(botSettings, probe, Hotspots);

                currentBot.Stop();

                OnPropertyChanged(nameof(StartCommandEnabled));
                OnPropertyChanged(nameof(StopCommandEnabled));
                //OnPropertyChanged(nameof(StartPowerlevelCommandEnabled));
                OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
                //OnPropertyChanged(nameof(ReloadBotsCommandEnabled));
                //OnPropertyChanged(nameof(CurrentBotEnabled));
                //OnPropertyChanged(nameof(GrindingHotspotEnabled));
                OnPropertyChanged(nameof(CurrentTravelPathEnabled));
            }
            catch (Exception e)
            {
                //Logger.Log(e);
            }
        }

        public ICommand StopCommand =>
            stopCommand ?? (stopCommand = new CommandHandler(UiStop, true));


        
        

        [BotSetting]
        public TravelPath CurrentTravelPath
        {
            get => botSettings.CurrentTravelPath;
            set
            {
                botSettings.CurrentTravelPath = value;
                OnPropertyChanged(nameof(CurrentTravelPath));
                OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
            }
        }

        bool reverseTravelPath;
        public bool ReverseTravelPath
        {
            get => reverseTravelPath;
            set
            {
                reverseTravelPath = value;
                OnPropertyChanged(nameof(ReverseTravelPath));
            }
        }
        public bool StartRecordingTravelPathCommandEnabled => !TravelPathGenerator.Recording;
        string newTravelPathName;
        public string NewTravelPathName
        {
            get => newTravelPathName;
            set
            {
                newTravelPathName = value;
                OnPropertyChanged(nameof(NewTravelPathName));
            }
        }
        public bool SaveTravelPathCommandEnabled =>
            TravelPathGenerator.Recording &&
            TravelPathGenerator.PositionCount > 0 &&
            !string.IsNullOrWhiteSpace(newTravelPathName);
        public bool CancelTravelPathCommandEnabled => TravelPathGenerator.Recording;
        public bool StartTravelPathCommandEnabled =>
            !currentBot.Running() &&
            CurrentTravelPath != null;
        public bool StopTravelPathCommandEnabled => currentBot.Running();

        public bool CurrentTravelPathEnabled => !currentBot.Running();

        ICommand startRecordingTravelPathCommand;

        void StartRecordingTravelPath()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    TravelPathGenerator.StartRecord(ObjectManager.Player);

                    OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(ListPathCommandEnabled));
                    OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));

                    Log("Start Record new travel path...");

                }
                else
                    Log("Start Record failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand StartRecordingTravelPathCommand =>
            startRecordingTravelPathCommand ?? (startRecordingTravelPathCommand = new CommandHandler(StartRecordingTravelPath, true));

        public bool RecordTravelPathCommandEnabled => TravelPathGenerator.Recording;

        ICommand recordTravelPathCommand;

        void RecordTravelPath()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    TravelPathGenerator.Record(ObjectManager.Player, Log);

                    OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(ListPathCommandEnabled));
                    OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));

                }
                else
                    Log("Record failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand EraseTravelPathCommand =>
            eraseTravelPathCommand ?? (eraseTravelPathCommand = new CommandHandler(EraseTravelPath, true));

        public bool EraseTravelPathCommandEnabled => TravelPathGenerator.Recording;

        ICommand eraseTravelPathCommand;

        void EraseTravelPath()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    TravelPathGenerator.Erase(ObjectManager.Player, Log);

                    OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(ListPathCommandEnabled));
                    OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));

                }
                else
                    Log("Record failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand ListPathCommand =>
            listPathCommand ?? (listPathCommand = new CommandHandler(ListPath, true));

        public bool ListPathCommandEnabled => TravelPathGenerator.Recording;

        ICommand listPathCommand;

        void ListPath()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    TravelPathGenerator.ListPath(ObjectManager.Player, Log);

                    OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(ListPathCommandEnabled));
                    OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                    OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));

                }
                else
                    Log("List Path failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }


        public ICommand RecordTravelPathCommand =>
            recordTravelPathCommand ?? (recordTravelPathCommand = new CommandHandler(RecordTravelPath, true));

        // CancelTravelPath command
        ICommand cancelTravelPathCommand;

        void CancelTravelPath()
        {
            try
            {
                TravelPathGenerator.Cancel();

                OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                OnPropertyChanged(nameof(ListPathCommandEnabled));
                OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));

                Log("Canceling new travel path...");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand CancelTravelPathCommand =>
            cancelTravelPathCommand ?? (cancelTravelPathCommand = new CommandHandler(CancelTravelPath, true));

        // SaveTravelPath command
        ICommand saveTravelPathCommand;

        void SaveTravelPath()
        {
            try
            {
                var waypoints = TravelPathGenerator.Save();
                var travelPath = Repository.AddTravelPath(newTravelPathName, waypoints);

                TravelPaths.Add(travelPath);
                TravelPaths = new ObservableCollection<TravelPath>(TravelPaths.OrderBy(p => p?.Name));

                NewTravelPathName = string.Empty;

                OnPropertyChanged(nameof(StartRecordingTravelPathCommandEnabled));
                OnPropertyChanged(nameof(RecordTravelPathCommandEnabled));
                OnPropertyChanged(nameof(EraseTravelPathCommandEnabled));
                OnPropertyChanged(nameof(ListPathCommandEnabled));
                OnPropertyChanged(nameof(SaveTravelPathCommandEnabled));
                OnPropertyChanged(nameof(CancelTravelPathCommandEnabled));
                OnPropertyChanged(nameof(TravelPaths));

                Log("New travel path successfully saved!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand SaveTravelPathCommand =>
            saveTravelPathCommand ?? (saveTravelPathCommand = new CommandHandler(SaveTravelPath, true));

        // StartTravelPath
        ICommand startTravelPathCommand;

        void StartTravelPath()
        {
            try
            {
                //TO DO

                

                Log("Travel started!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand StartTravelPathCommand =>
            startTravelPathCommand ?? (startTravelPathCommand = new CommandHandler(StartTravelPath, true));

        // StopTravelPath
        ICommand stopTravelPathCommand;

        void StopTravelPath()
        {
            try
            {
                //TO DO

                

                Log("TravelPath stopped!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand StopTravelPathCommand =>
            stopTravelPathCommand ?? (stopTravelPathCommand = new CommandHandler(StopTravelPath, true));

        public ICommand ClearLogCommand =>
            clearLogCommand ?? (clearLogCommand = new CommandHandler(ClearLog, true));
        void ClearLog()
        {
            try
            {
                ConsoleOutput.Clear();
                OnPropertyChanged(nameof(ConsoleOutput));
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }



        public void WriteTxt(string logstring, string newPath)
        {
            string path = newPath + "\\" + "test.txt";
            if (!File.Exists(path))
            {
                FileStream stream = File.Create(path);
                stream.Close();
                stream.Dispose();
            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(logstring);
                writer.Close();
            }

        }
        public class BotSettingAttribute : Attribute
        {
        }
        public class ProbeFieldAttribute : Attribute
        {
        }

        [ProbeField]
        public string CurrentState
        {
            get => probe.CurrentState;
        }

        [ProbeField]
        public string CurrentPosition
        {
            get => probe.CurrentPosition;
        }

        [ProbeField]
        public string CurrentZone
        {
            get => probe.CurrentZone;
        }

        [ProbeField]
        public string TargetName
        {
            get => probe.TargetName;
        }

        [ProbeField]
        public string TargetClass
        {
            get => probe.TargetClass;
        }

        [ProbeField]
        public string TargetCreatureType
        {
            get => probe.TargetCreatureType;
        }

        [ProbeField]
        public string TargetPosition
        {
            get => probe.TargetPosition;
        }

        [ProbeField]
        public string TargetRange
        {
            get => probe.TargetRange;
        }

        [ProbeField]
        public string TargetFactionId
        {
            get => probe.TargetFactionId;
        }

        [ProbeField]
        public string TargetIsCasting
        {
            get => probe.TargetIsCasting;
        }

        [ProbeField]
        public string TargetIsChanneling
        {
            get => probe.TargetIsChanneling;
        }
    }
}