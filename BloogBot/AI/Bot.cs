using BloogBot.AI.SharedStates;
using BloogBot.Game;
using BloogBot.Game.Enums;
using BloogBot.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BloogBot.AI
{
    public abstract class Bot
    {
        readonly Stack<IBotState> botStates = new Stack<IBotState>();

        bool running;
        bool nextState;
        bool retrievingCorpse;
        Type stackState;
        Type currentState;
        int currentStateStartTime;
        Position currentPosition;
        int currentPositionStartTime;
        Position teleportCheckPosition;
        bool isFalling;
        int currentLevel;

        Action stopCallback;

        public bool Running() => running;


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

        public void StartPowerlevel(IDependencyContainer container, Action stopCallback)
        {
            this.stopCallback = stopCallback;

            try
            {
                running = true;
                /*
                ThreadSynchronizer.RunOnMainThread(() =>
                {
                    botStates.Push(new PowerlevelState(botStates, container));

                    currentState = botStates.Peek().GetType();
                    currentStateStartTime = Environment.TickCount;
                    currentPosition = ObjectManager.Player.Position;
                    currentPositionStartTime = Environment.TickCount;
                    teleportCheckPosition = ObjectManager.Player.Position;
                });

                StartPowerlevelInternal(container);
                */
            }
            catch (Exception e)
            {
                //Logger.Log(e);
            }
        }

        public void StartTestBot(ActionList actionList, Action stopCallback)
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
                    nextState = false;
                    currentState = botStates.Peek().GetType();
                    stackState = currentState;
                    currentStateStartTime = Environment.TickCount;
                    currentPosition = ObjectManager.Player.UnitPosition;
                    currentPositionStartTime = Environment.TickCount;
                    teleportCheckPosition = ObjectManager.Player.UnitPosition;
                });
                TestBotInternal(actionList);
            }
            catch (Exception e)
            {
                //Logger.Log(e);
            }
        }

        async public void TestBotInternal(ActionList actionList)
        {
            while (running)
            {
                try
                {
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
                        }

                        if (currentState == stackState && nextState)
                        {
                            botStates.Peek()?.Update();
                            nextState = false;
                        }

                        if (botStates.Count > 0 && currentState != stackState)
                        {
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

        public void TestBotNextState()
        {
            nextState = true;
        }


        public void Stop()
        {
            running = false;
            currentLevel = 0;

            while (botStates.Count > 0)
                botStates.Pop();

            stopCallback?.Invoke();
        }

        public void StopTest()
        {
            running = false;
            currentLevel = 0;

            while (botStates.Count > 0)
                botStates.Pop();

            stopCallback?.Invoke();
        }

        void LogToFile(string text)
        {
            var dir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MainViewModel)).CodeBase);
            var path = new UriBuilder(dir).Path;
            var file = Path.Combine(path, "StuckLog.txt");

            using (var sw = File.AppendText(file))
            {
                sw.WriteLine(text);
            }
        }
        public void PopStackToBaseState()
        {
            while (botStates.Count > 1)
                botStates.Pop();
        }
    }
}
