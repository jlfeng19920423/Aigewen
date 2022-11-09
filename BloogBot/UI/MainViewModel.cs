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

            currentBot = botLoader.ReloadBot();
            currentFunc = funcLoader.ReloadFuncTest();
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
            ObjectManager.StartTraverseObjMgr();
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
            try 
            {
                currentFunc.FuncTest(SpellId, Log);

            }
            catch (Exception e)
            {
                //Logger.Log(e + "\n");
            }
        }

        IntPtr BobberEntryPtr = IntPtr.Zero;
        
        byte ret = 0;
        void GetTargetEnt()
        {
            try
            {
                currentFunc.GetTargetEnt(Log);

            }
            catch (Exception e)
            {
                //Logger.Log(e + "\n");
            }

        }

        
        void Objects()
        {
            var CurMgr = MemoryManager.ReadIntPtr(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Object_Manager.Base));
            var Count = MemoryManager.ReadInt(CurMgr);
            var ArrayAddr = MemoryManager.ReadIntPtr(IntPtr.Add(CurMgr, array));
            var PlayerGuid1 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid));
            var PlayerGuid2 = MemoryManager.ReadUlong(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Player_Guid) + 0x8);
            var TargetGuid = MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.Guids.Target_Guid));
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
                    
                    if (guid1 == TargetGuid.low && guid2 == TargetGuid.high && type == ObjectType.Unit)//info for Unit
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
                    
                    if (guid1 == TargetGuid.low && guid2 == TargetGuid.high && type == ObjectType.Player)//info for player
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

        ICommand reloadFuncCommand;
        public ICommand ReloadFuncCommand =>
            reloadFuncCommand ?? (reloadFuncCommand = new CommandHandler(ReloadFunc, true));

        IFuncTest currentFunc;
        readonly FuncTestLoader funcLoader = new FuncTestLoader();
        void ReloadFunc()
        {
            try
            {
                currentFunc = funcLoader.ReloadFuncTest();
                Log("FuncTest successfully loaded!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }




        /// <summary>
        /// Tab:Overview
        /// </summary>
        IBot currentBot;
        readonly BotLoader botLoader = new BotLoader();

        public bool StartCommandEnabled => !currentBot.Running();

        public bool StopCommandEnabled => currentBot.Running();

        //Start command
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
                    //OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                    //OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
                    //OnPropertyChanged(nameof(ReloadBotsCommandEnabled));
                    //OnPropertyChanged(nameof(CurrentBotEnabled));
                    //OnPropertyChanged(nameof(GrindingHotspotEnabled));
                    OnPropertyChanged(nameof(CurrentTravelPathEnabled));
                }

                currentBot.Start(currentBot.actionList, stopCallback);

                OnPropertyChanged(nameof(StartCommandEnabled));
                OnPropertyChanged(nameof(StopCommandEnabled));
                //OnPropertyChanged(nameof(StartPowerlevelCommandEnabled));
                //OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                //OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
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
                //OnPropertyChanged(nameof(StartTravelPathCommandEnabled));
                //OnPropertyChanged(nameof(StopTravelPathCommandEnabled));
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
        
        //reloadbot command
        ICommand reloadBotsCommand;
        void ReloadBots()
        {
            try
            {
                currentBot = botLoader.ReloadBot();

                //OnPropertyChanged(nameof(Bots));
                OnPropertyChanged(nameof(StartCommandEnabled));
                OnPropertyChanged(nameof(StopCommandEnabled));
                //OnPropertyChanged(nameof(StartPowerlevelCommandEnabled));
                //OnPropertyChanged(nameof(ReloadBotsCommandEnabled));

                Log("Bot successfully loaded!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        public ICommand ReloadBotsCommand =>
            reloadBotsCommand ?? (reloadBotsCommand = new CommandHandler(ReloadBots, true));


        /// <summary>
        /// Tab: ActionList
        /// </summary>

        string newActionListName;
        public string NewActionListName
        {
            get => newActionListName;
            set
            {
                newActionListName = value;
                OnPropertyChanged(nameof(NewActionListName));
            }
        }
        int actionIndex;
        public int ActionIndex
        {
            get => actionIndex;
            set
            {
                actionIndex = value;
                OnPropertyChanged(nameof(ActionIndex));
            }
        }
        int nextActionIndex;
        public int NextActionIndex
        {
            get => nextActionIndex;
            set
            {
                nextActionIndex = value;
                OnPropertyChanged(nameof(NextActionIndex));
            }
        }

        string actionName;
        public string ActionName
        {
            get => actionName;
            set
            {
                actionName = value;
                OnPropertyChanged(nameof(ActionName));
            }
        }
        
        int insertActionIndex;
        public int InsertActionIndex
        {
            get => insertActionIndex;
            set
            {
                insertActionIndex = value;
                OnPropertyChanged(nameof(InsertActionIndex));
            }
        }

        int insertPositionIndex;
        public int InsertPositionIndex
        {
            get => insertPositionIndex;
            set
            {
                insertPositionIndex = value;
                OnPropertyChanged(nameof(InsertPositionIndex));
            }
        }

        int eraseActionIndex;
        public int EraseActionIndex
        {
            get => eraseActionIndex;
            set
            {
                eraseActionIndex = value;
                OnPropertyChanged(nameof(EraseActionIndex));
            }
        }


        const string BOT_PATH = @"C:\Users\tommy\Documents\GitHub\Aigewen\bin\Debug\Debug\net6.0\FishingBot.dll";

        public bool CurrentTravelPathEnabled => !currentBot.Running();

        ICommand startRecordingActionListCommand;
        public ICommand StartRecordingActionListCommand =>
            startRecordingActionListCommand ?? (startRecordingActionListCommand = new CommandHandler(StartRecordingActionList, true));
        public bool StartRecordingActionListCommandEnabled => !ActionListGenerator.Recording;
        void StartRecordingActionList()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.StartRecord(ObjectManager.Player);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                    Log("Start Record new action list...");

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


        ICommand addActionCommand;
        public ICommand AddActionCommand =>
            addActionCommand ?? (addActionCommand = new CommandHandler(AddAction, true));
        public bool AddActionCommandEnabled => ActionListGenerator.Recording;
        void AddAction()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.AddAction(ObjectManager.Player, Log, actionName);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("AddAction failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        ICommand insertActionAtCommand;
        public ICommand InsertActionAtCommand =>
            insertActionAtCommand ?? (insertActionAtCommand = new CommandHandler(InsertActionAt, true));
        public bool InsertActionAtCommandEnabled => ActionListGenerator.Recording;
        void InsertActionAt()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.InsertActionAt(ObjectManager.Player, Log, insertActionIndex, actionName);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("AddAction failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }



        ICommand addPositionCommand;
        public ICommand AddPositionCommand =>
            addPositionCommand ?? (addPositionCommand = new CommandHandler(AddPosition, true));
        public bool AddPositionCommandEnabled => ActionListGenerator.Recording;
        void AddPosition()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.AddPosition(ObjectManager.Player, Log, actionName);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("AddPosition failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        ICommand insertPositionAtCommand;
        public ICommand InsertPositionAtCommand =>
            insertPositionAtCommand ?? (insertPositionAtCommand = new CommandHandler(InsertPositionAt, true));
        public bool InsertPositionAtCommandEnabled => ActionListGenerator.Recording;
        void InsertPositionAt()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.InsertPositionAt(ObjectManager.Player, Log, insertPositionIndex);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("AddAction failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }


        public bool EraseActionCommandEnabled => ActionListGenerator.Recording;

        ICommand eraseActionCommand;
        public ICommand EraseActionCommand =>
            eraseActionCommand ?? (eraseActionCommand = new CommandHandler(EraseAction, true));
        void EraseAction()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.EraseAction(ObjectManager.Player, Log, eraseActionIndex);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("Erase Action failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        

        public bool ListActionCommandEnabled => ActionListGenerator.Recording;

        ICommand listActionCommand;
        public ICommand ListActionCommand =>
            listActionCommand ?? (listActionCommand = new CommandHandler(ListAction, true));

        void ListAction()
        {
            try
            {
                var isLoggedIn = ObjectManager.IsLoggedIn;
                if (isLoggedIn)
                {
                    ActionListGenerator.ListAction(ObjectManager.Player, Log);

                    OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                    OnPropertyChanged(nameof(AddActionCommandEnabled));
                    OnPropertyChanged(nameof(AddPositionCommandEnabled));
                    OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                    OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                    OnPropertyChanged(nameof(EraseActionCommandEnabled));
                    OnPropertyChanged(nameof(ListActionCommandEnabled));
                    OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                    OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                }
                else
                    Log("List Action failed. Not logged in.");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }


        

        // CancelActionList command
        ICommand cancelActionListCommand;
        public ICommand CancelActionListCommand =>
            cancelActionListCommand ?? (cancelActionListCommand = new CommandHandler(CancelActionList, true));
        public bool CancelActionListCommandEnabled => ActionListGenerator.Recording;
        void CancelActionList()
        {
            try
            {
                ActionListGenerator.Cancel();

                OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                OnPropertyChanged(nameof(AddActionCommandEnabled));
                OnPropertyChanged(nameof(AddPositionCommandEnabled));
                OnPropertyChanged(nameof(InsertActionAtCommandEnabled));
                OnPropertyChanged(nameof(InsertPositionAtCommandEnabled));
                OnPropertyChanged(nameof(EraseActionCommandEnabled));
                OnPropertyChanged(nameof(ListActionCommandEnabled));
                OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                OnPropertyChanged(nameof(CancelActionListCommandEnabled));

                Log("Canceling new action list...");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        

        // SaveActionList command
        ICommand saveActionListCommand;
        public ICommand SaveActionListCommand =>
            saveActionListCommand ?? (saveActionListCommand = new CommandHandler(SaveActionList, true));
        public bool SaveActionListCommandEnabled =>
            ActionListGenerator.Recording &&
            ActionListGenerator.ActionCount > 0 &&
            !string.IsNullOrWhiteSpace(newActionListName);
        void SaveActionList()
        {
            try
            {
                ActionListGenerator.Save(newActionListName);


                NewActionListName = string.Empty;

                OnPropertyChanged(nameof(StartRecordingActionListCommandEnabled));
                OnPropertyChanged(nameof(AddActionCommandEnabled));
                OnPropertyChanged(nameof(EraseActionCommandEnabled));
                OnPropertyChanged(nameof(ListActionCommandEnabled));
                OnPropertyChanged(nameof(SaveActionListCommandEnabled));
                OnPropertyChanged(nameof(CancelActionListCommandEnabled));
                OnPropertyChanged(nameof(TravelPaths));

                Log("New travel path successfully saved!");
            }
            catch (Exception e)
            {
                //Logger.Log(e);
                //Log(COMMAND_ERROR);
            }
        }

        

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