using BloogBot;
using BloogBot.AI;
using BloogBot.AI.SharedStates;
using BloogBot.Game;
using BloogBot.Game.Objects;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FishingBot
{
    [Export(typeof(IBot))]
    public class FishingBot : Bot, IBot
    {
        readonly Stack<IBotState> botStates = new Stack<IBotState>();

        bool running;
        public bool Running() => running;

        Type currentState;
        int currentStateStartTime;
        Position currentPosition;
        int currentPositionStartTime;
        Position teleportCheckPosition;
        int currentLevel;

        public FishingBot()
        {
            initialActionList();
        }

        Action stopCallback;
        public string Name => "Fishing";
        public string FileName => "Fishing.dll";
        bool AdditionalTargetingCriteria(WoWUnit unit) => true;
        IBotState CreateRestState(Stack<IBotState> botStates, IDependencyContainer container) =>
            new RestState(botStates, container);

        IBotState CreateRetrieveCorpseState(Stack<IBotState> botStates, IDependencyContainer container) =>
            new RetrieveCorpseState(botStates, container);

        IBotState CreateMoveToCorpseState(Stack<IBotState> botStates, IDependencyContainer container) =>
            new MoveToCorpseState(botStates, container);

        IBotState CreateMoveToTargetState(Stack<IBotState> botStates, IDependencyContainer container, WoWUnit target) =>
            new MoveToTargetState(botStates, container, target);

        IBotState CreatePowerlevelCombatState(Stack<IBotState> botStates, IDependencyContainer container, WoWUnit target, WoWPlayer powerlevelTarget) =>
            new PowerlevelCombatState(botStates, container, target, powerlevelTarget);
        public IDependencyContainer GetDependencyContainer(Probe probe) =>
            new DependencyContainer(
                AdditionalTargetingCriteria,
                CreateRestState,
                CreateMoveToTargetState,
                //CreatePowerlevelCombatState,
                //botSettings,
                probe);
        //hotspots);

        public List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack = new List<Func<Stack<IBotState>, ActionList, IBotState>>();

        public void Start(ActionList actionList, Action stopCallback)
        {
            this.stopCallback = stopCallback;
            try
            {
                running = true;

                var closestWaypoint = actionList.Waypoints
                    .OrderBy(w => w.DistanceTo(ObjectManager.Player.UnitPosition))
                    .First();
                var startingIndex = actionList.Waypoints
                    .ToList()
                    .IndexOf(closestWaypoint);

                actionList.ActionIndex = startingIndex;

                ThreadSynchronizer.RunOnMainThread(() =>
                {
                    currentLevel = ObjectManager.Player.Level;

                    /*
                    void callbackInternal()
                    {
                        running = false;
                        currentState = null;
                        currentPosition = null;
                        callback();
                    }
                    */

                    botStates.Push(new ActionStackState(botStates, actionList));

                    currentState = botStates.Peek().GetType();
                    currentStateStartTime = Environment.TickCount;
                    currentPosition = ObjectManager.Player.UnitPosition;
                    currentPositionStartTime = Environment.TickCount;
                    teleportCheckPosition = ObjectManager.Player.UnitPosition;
                });
                StartInternal(actionList);
            }
            catch (Exception e)
            {
                //Logger.Log(e);
            }
        }
        async public void StartInternal(ActionList actionList)
        {
            while (running)
            {
                try
                {
                    //Console.WriteLine("actionIndex{0}, Count:{1}", actionList.ActionIndex, botStates.Count());
                    //Console.WriteLine(botStates.Peek()?.GetType().Name);
                    ThreadSynchronizer.RunOnMainThread(() =>
                    {
                        if (botStates.Count() == 0 || actionList.ActionIndex == -1)
                        {
                            Stop();
                            return;
                        }

                        var player = ObjectManager.Player;

                        if (botStates.Peek().GetType() != currentState)
                        {
                            currentState = botStates.Peek().GetType();
                            currentStateStartTime = Environment.TickCount;
                        }

                        if (botStates.Count > 0)
                        {
                            //container.Probe.CurrentState = botStates.Peek()?.GetType().Name;
                            botStates.Peek()?.Update();
                        }
                    });
                    await Task.Delay(100);
                }
                catch (Exception e)
                {
                    //Logger.Log(e + "\n");
                }
            }
        }

        public void Stop()
        {
            running = false;
            currentLevel = 0;

            while (botStates.Count > 0)
                botStates.Pop();

            stopCallback?.Invoke();
        }

        public void Test(IDependencyContainer container) { }


        static public List<Position> waypoints = new List<Position>();
        static public List<int> actionIndexList = new List<int>();
        static public List<int> nextactionIndexList = new List<int>();

        public ActionList actionList => initialActionList();

        ActionList initialActionList()
        {
            ActionList actions;

            IBotState CreateMoveToPositionState(Stack<IBotState> botStates, ActionList ActionList) => new MoveToPositionState(botStates, ActionList);
            IBotState CreateFaceTo(Stack<IBotState> botStates, ActionList ActionList) => new FaceTo(botStates, ActionList);
            IBotState CreateFishingState(Stack<IBotState> botStates, ActionList ActionList) => new FishingState(botStates, ActionList);

            
            waypoints.Add(new Position((float)-8838.341, (float)634.9344, (float)94.64573, 0));
            waypoints.Add(new Position((float)-8842.917, (float)641.5321, (float)95.70744, 1));
            waypoints.Add(new Position((float)-8846.967, (float)651.1913, (float)96.78641, 2));
            waypoints.Add(new Position((float)-8849.237, (float)661.3346, (float)97.32548, 3));
            waypoints.Add(new Position((float)-8827.677, (float)678.1038, (float)97.46655, 4));
            waypoints.Add(new Position((float)-8841.397, (float)716.8478, (float)97.5912, 5));
            waypoints.Add(new Position((float)-8836.563, (float)727.4146, (float)97.6856, 6));
            waypoints.Add(new Position((float)-8801.275, (float)745.8544, (float)97.59063, 7));
            waypoints.Add(new Position((float)-8792.931, (float)771.1873, (float)96.33835, 8));

            actionIndexList.Add(0);
            actionIndexList.Add(1);
            actionIndexList.Add(2);
            actionIndexList.Add(3);
            actionIndexList.Add(4);
            actionIndexList.Add(5);
            actionIndexList.Add(6);
            actionIndexList.Add(7);
            actionIndexList.Add(8);
            actionIndexList.Add(9);
            actionIndexList.Add(10);
            

            nextactionIndexList.Add(1);
            nextactionIndexList.Add(2);
            nextactionIndexList.Add(3);
            nextactionIndexList.Add(4);
            nextactionIndexList.Add(5);
            nextactionIndexList.Add(6);
            nextactionIndexList.Add(7);
            nextactionIndexList.Add(8);
            nextactionIndexList.Add(9);
            nextactionIndexList.Add(10);
            nextactionIndexList.Add(-1);

            stateStack.Add(CreateMoveToPositionState); //index 0
            stateStack.Add(CreateMoveToPositionState); //index 1
            stateStack.Add(CreateMoveToPositionState); //index 2
            stateStack.Add(CreateMoveToPositionState); //index 3
            stateStack.Add(CreateMoveToPositionState); //index 4
            stateStack.Add(CreateMoveToPositionState); //index 5
            stateStack.Add(CreateMoveToPositionState); //index 6
            stateStack.Add(CreateMoveToPositionState); //index 7
            stateStack.Add(CreateMoveToPositionState); //index 8
            stateStack.Add(CreateFaceTo); //index 9
            stateStack.Add(CreateFishingState); //index 10
            


            actions = new ActionList(waypoints, actionIndexList, nextactionIndexList, stateStack);
            actions.ActionIndex = 0;

            return actions;
        }



    }

    class MoveToTargetState : IBotState
    {
        const string SummonImp = "Summon Imp";
        const string SummonVoidwalker = "Summon Voidwalker";
        const int CurseOfAgony = 123;
        const int ShadowBolt = 321;

        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;
        readonly WoWUnit target;
        readonly LocalPlayer player;
        readonly StuckHelper stuckHelper;

        readonly int pullingSpell;

        internal MoveToTargetState(Stack<IBotState> botStates, IDependencyContainer container, WoWUnit target)
        {
            this.botStates = botStates;
            this.container = container;
            this.target = target;
            player = ObjectManager.Player;
            stuckHelper = new StuckHelper(botStates, container);
            if (player.KnowsSpell(CurseOfAgony, false))
                pullingSpell = CurseOfAgony;
            else
                pullingSpell = ShadowBolt;
        }

        public void Update()
        {

        }
    }

    class RetrieveCorpseState : IBotState
    {
        const int resDistance = 30;

        // res distance is around 36 units, so we build up a grid of 38 units 
        // in every direction, adding 1 to account for the center.
        static readonly int length = Convert.ToInt32(Math.Pow((resDistance * 2) + 1, 2.0));
        readonly Position[] resLocs = new Position[length];
        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;
        readonly LocalPlayer player;

        bool initialized;

        public RetrieveCorpseState(Stack<IBotState> botStates, IDependencyContainer container)
        {
            this.botStates = botStates;
            this.container = container;
            player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!initialized)
            {
                // corpse position is wrong immediately after releasing, so we wait for 5s.
                //Thread.Sleep(5000);

                var resLocation = player.CorpsePosition;

                return;
            }

            if (Wait.For("StartRetrieveCorpseStateDelay", 1000))
            {
                if (ObjectManager.Player.IsGhost)
                    ObjectManager.Player.RetrieveCorpse();
                else
                {
                    if (Wait.For("LeaveRetrieveCorpseStateDelay", 2000))
                    {
                        botStates.Pop();
                        return;
                    }
                }
            }
        }
    }

    class MoveToCorpseState : IBotState
    {
        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;
        readonly LocalPlayer player;
        readonly StuckHelper stuckHelper;

        bool walkingOnWater;
        int stuckCount;

        bool initialized;

        public MoveToCorpseState(Stack<IBotState> botStates, IDependencyContainer container)
        {
            this.botStates = botStates;
            this.container = container;
            player = ObjectManager.Player;
            stuckHelper = new StuckHelper(botStates, container);
        }

        public void Update()
        {
            if (!initialized)
            {
                container.DisableTeleportChecker = false;
                initialized = true;
            }

            if (stuckCount == 10)
            {
                while (botStates.Count > 0)
                    botStates.Pop();

                return;
            }

            if (stuckHelper.CheckIfStuck())
                stuckCount++;

            if (player.UnitPosition.DistanceTo2D(player.CorpsePosition) < 3)
            {
                //player.StopAllMovement();
                botStates.Pop();
                return;
            }

            // TO DO
        }
    }

    class PowerlevelCombatState : IBotState
    {
        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;
        readonly WoWUnit target;

        public PowerlevelCombatState(Stack<IBotState> botStates, IDependencyContainer container, WoWUnit target, WoWPlayer powerlevelTarget)
        {
            this.botStates = botStates;
            this.container = container;
            this.target = target;
        }

        public void Update()
        {
            // TODO
        }
    }

    class RestState : IBotState
    {
        const int stackCount = 5;

        const string ConsumeShadows = "Consume Shadows";
        const string HealthFunnel = "Health Funnel";

        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;
        readonly LocalPlayer player;

        //readonly WoWItem foodItem;
        //readonly WoWItem drinkItem;
        //LocalPet pet;

        public RestState(Stack<IBotState> botStates, IDependencyContainer container)
        {
            this.botStates = botStates;
            this.container = container;
            player = ObjectManager.Player;
            //player.SetTarget(player.Guid);

            //foodItem = Inventory.GetAllItems()
            //    .FirstOrDefault(i => i.Info.Name == container.BotSettings.Food);

            //drinkItem = Inventory.GetAllItems()
            //    .FirstOrDefault(i => i.Info.Name == container.BotSettings.Drink);
        }

        public void Update()
        {

        }

        //bool HealthOk => foodItem == null || player.HealthPercent >= 90 || (player.HealthPercent >= 70 && !player.IsEating);

        //bool PetHealthOk => ObjectManager.Pet == null || ObjectManager.Pet.HealthPercent >= 80;

        //bool ManaOk => (player.Level < 6 && player.ManaPercent > 50) || player.ManaPercent >= 90 || (player.ManaPercent >= 55 && !player.IsDrinking);

        //bool InCombat => ObjectManager.Player.IsInCombat || ObjectManager.Units.Any(u => u.TargetGuid == ObjectManager.Player.Guid || u.TargetGuid == ObjectManager.Pet?.Guid);
    }

    class FishingState : IBotState
    {
        readonly Stack<IBotState> botStates;
        //readonly IDependencyContainer container;
        readonly LocalPlayer player;
        WoWGameObject gameObject;
        readonly ActionList actionList;
        bool fishing;
        byte Animating = 0;


        State state = State.Uninitialized;
        //int stuckCount;

        //public MoveToPositionState(Stack<IBotState> botStates, IDependencyContainer container, Position destination, bool use2DPop = false)
        public FishingState(Stack<IBotState> botStates, ActionList actionList)
        {
            this.botStates = botStates;
            this.actionList = actionList;
            player = ObjectManager.Player;
            this.fishing = false;
        }

        public void Update()
        {
            int fishingspell = GetFishingLevel();
            //int fishingspell = 7620;
            //Console.WriteLine("fishingspell:{0}",fishingspell);
            //Console.WriteLine("FishingState");
            if (player.MoveFlag > 0) return;

            if (state == State.Uninitialized)
            {
                if (player.ChanID == fishingspell && this.fishing == true)
                {
                    state = State.Fishing;
                }
                else
                {
                    state = State.NotFishing;
                }
            }


            if (state == State.NotFishing && this.fishing == false)
            {
                var SpellSlot = Functions.FindSlotBySpellId(fishingspell, false);
                Functions.CastSpellBySlot(SpellSlot, player.TargetGuidPtr);
                this.fishing = true;
                state = State.Fishing;
            }


            if (gameObject == null)
            {
                if (state == State.Fishing && Wait.For("DelayForSpell", 500))
                {
                    gameObject = ObjectManager.GameObjects.FirstOrDefault(u => u.Creator.isEqualTo(player.Guid) && u.Name == "鱼漂");
                }
            }

            if (gameObject != null)
            {

                Animating = gameObject.Animating;
                //Console.WriteLine("Animating:{0}", Animating);
                if (Animating != 0)
                {
                    var ret = Functions.GameUIOnSpriteRightClick(gameObject.GuidPtr);
                    state = State.ReadyToPop;
                }
            }
            //if(state ==State.ReadyToPop) Console.WriteLine("state:{0}, Count:{1}", state, Wait.Count());
            if (state == State.ReadyToPop && Wait.For("LootItemsDelay", 2000))
            {
                //Console.WriteLine("pop state");
                Wait.RemoveAll();
                botStates.Pop();
            }

            if (state == State.Fishing && player.ChanID != fishingspell && gameObject != null)
            {
                //Console.WriteLine("pop state2");
                Wait.RemoveAll();
                botStates.Pop();
            }
        }

        int GetFishingLevel()
        {
            if (player.KnowsSpell((int)FishingLevels.master, false))
            {
                return (int)FishingLevels.master;
            }
            else if (player.KnowsSpell((int)FishingLevels.artisan, false))
            {
                return (int)FishingLevels.artisan;
            }
            else if (player.KnowsSpell((int)FishingLevels.expert, false))
            {
                return (int)FishingLevels.expert;
            }
            else if (player.KnowsSpell((int)FishingLevels.journeyman, false))
            {
                return (int)FishingLevels.journeyman;
            }
            else if (player.KnowsSpell((int)FishingLevels.apprentice, false))
            {
                return (int)FishingLevels.apprentice;
            }
            else
            {
                return (int)FishingLevels.apprentice;
            }
        }

        enum State
        {
            Uninitialized,
            Fishing,
            NotFishing,
            Looting,
            ReadyToPop,
        }
    }

    class FaceTo : IBotState
    {
        readonly Stack<IBotState> botStates;
        //readonly IDependencyContainer container;
        readonly LocalPlayer player;
        readonly ActionList actionList;

        public FaceTo(Stack<IBotState> botStates, ActionList actionList)
        {
            this.botStates = botStates;
            this.actionList = actionList;
            player = ObjectManager.Player;
        }

        public void Update()
        {
            if (player.MoveFlag > 0) return;

            float facing = (float)1.6;

            if (player.RotationF != facing)
            {
                Functions.FaceTo(player.EntPtr, (float)1.6);
            }
            else
            {
                botStates.Pop();
                actionList.ActionIndex = actionList.NextActionIndexList.ElementAt(actionList.ActionIndex);
                return;
            }
        }

    }


    public enum FishingLevels : int
    {
        apprentice = 7620,
        journeyman = 7731,
        expert = 7732,
        artisan = 18248,
        master = 33095
    }
}