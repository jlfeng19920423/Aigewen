using BloogBot.Game;
using BloogBot.Game.Objects;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace BloogBot.AI
{
    public static class ActionListGenerator
    {
        static Action callback;
        static Position previousPosition;
        static public List<Position> waypoints = new List<Position>();
        static public List<int> actionIndexList = new List<int>();
        static public List<int> nextactionIndexList = new List<int>();
        public static List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack = new List<Func<Stack<IBotState>, ActionList, IBotState>>();

        public static List<string> StackNames = new List<string>();
        static public void Initialize(Action parCallback)
        {
            callback = parCallback;
        }

        static public bool Recording { get; private set; }

        static public int ActionCount => actionIndexList.Count;
        static public int PositionCount => waypoints.Count;


        static public void StartRecord(WoWPlayer player)
        {
            Recording = true;
            StackNames.Clear();
            waypoints.Clear();
            actionIndexList.Clear();
            nextactionIndexList.Clear();
        }

        static public void AddAction(WoWPlayer player, Action<string> log, string botState)
        {
            if (Recording)
            {
                int actionIndex = waypoints.Count;
                int nextActionIndex = waypoints.Count + 1;
                Position position = new Position(player.UnitPosition.X, player.UnitPosition.Y, player.UnitPosition.Z);

                waypoints.Add(position);
                actionIndexList.Add(actionIndex);
                nextactionIndexList.Add(nextActionIndex);
                StackNames.Add(botState);
                
                log("Adding action " + actionIndex + ": " + botState + ", nextid: " + nextActionIndex);
            }
        }

        static public void InsertActionAt(WoWPlayer player, Action<string> log, int insertActionIndex, string botState)
        {
            if (Recording)
            { 
                int actionIndex = insertActionIndex;
                int nextActionIndex = insertActionIndex + 1;
                Position position = new Position(player.UnitPosition.X, player.UnitPosition.Y, player.UnitPosition.Z);
                waypoints.Insert(insertActionIndex, position);
                actionIndexList.Insert(insertActionIndex, actionIndex);
                nextactionIndexList.Insert(insertActionIndex, nextActionIndex);
                StackNames.Insert(insertActionIndex, botState);
                for (int i = insertActionIndex + 1; i < actionIndexList.Count; i++)
                {
                    actionIndexList[i] += 1;
                    nextactionIndexList[i] += 1;
                }
                
                log("Insert action " + actionIndex + ":" + botState + ", nextid: " + nextActionIndex);
            }
        }

        static public void AddPosition(WoWPlayer player, Action<string> log, string botState)
        {
            if (Recording)
            {
                int actionIndex = waypoints.Count;
                int nextActionIndex = waypoints.Count + 1;
                Position position = new Position(player.UnitPosition.X, player.UnitPosition.Y, player.UnitPosition.Z);
                
                waypoints.Add(position);
                actionIndexList.Add(actionIndex);
                nextactionIndexList.Add(nextActionIndex);
                StackNames.Add("CreateMoveToPositionState");
                
                log("Adding waypoint " + actionIndex + ":" + waypoints.Last<Position>().X + "," + waypoints.Last<Position>().Y + "," + waypoints.Last<Position>().Z + ", nextid: " + nextActionIndex);
            }
        }

        static public void InsertPositionAt(WoWPlayer player, Action<string> log, int insertPositionIndex)
        {
            if (Recording)
            {
                int actionIndex = insertPositionIndex;
                int nextActionIndex = insertPositionIndex + 1;
                Position position = new Position(player.UnitPosition.X, player.UnitPosition.Y, player.UnitPosition.Z);
                waypoints.Insert(insertPositionIndex, position);
                actionIndexList.Insert(insertPositionIndex, actionIndex);
                nextactionIndexList.Insert(insertPositionIndex, nextActionIndex);
                StackNames.Insert(insertPositionIndex, "CreateMoveToPositionState");
                for (int i = insertPositionIndex + 1; i < actionIndexList.Count; i++)
                {
                    actionIndexList[i] += 1;
                    nextactionIndexList[i] += 1;
                }

                log("Insert waypoint " + actionIndex + ":" + waypoints.Last<Position>().X + "," + waypoints.Last<Position>().Y + "," + waypoints.Last<Position>().Z + ", nextid: " + nextActionIndex);
            }
        }

        static public void EraseAction(WoWPlayer player, Action<string> log, int eraseActionIndex)
        {
            if (Recording)
            {
                if (waypoints.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    waypoints.RemoveAt(eraseActionIndex) ;
                    actionIndexList.RemoveAt(eraseActionIndex);
                    nextactionIndexList.RemoveAt(eraseActionIndex);
                    StackNames.RemoveAt(eraseActionIndex);
                    for (int i = eraseActionIndex; i < actionIndexList.Count; i++)
                    {
                        actionIndexList[i] -= 1;
                        nextactionIndexList[i] -= 1;
                    }

                    log("erase action " + eraseActionIndex );
                }
            }
        }

        static public void ListAction(WoWPlayer player, Action<string> log)
        {
            if (Recording)
            {
                if (waypoints.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    var count = waypoints.Count;
                    for (int i = 0; i < count; i++)
                    {
                        log($"action {actionIndexList[i]}:{StackNames[i]}, Position:{waypoints[i].X},{waypoints[i].Y},{waypoints[i].Z}, nextid:{nextactionIndexList[i]}");
                    }
                }
            }
        }

        static public async void AutoRecord(WoWPlayer player, Action<string> log)
        {
            previousPosition = player.UnitPosition;

            while (Recording)
            {
                if (previousPosition.DistanceTo(player.UnitPosition) > 5)
                {
                    int actionIndex = waypoints.Count;
                    int nextActionIndex = waypoints.Count + 1;
                    waypoints.Add(player.UnitPosition);
                    actionIndexList.Add(actionIndex);
                    nextactionIndexList.Add(nextActionIndex);
                    StackNames.Add("CreateMoveToPositionState");
                    previousPosition = player.UnitPosition;
                    log("Auto record waypoint " + actionIndex + ":" + waypoints.Last<Position>().X + "," + waypoints.Last<Position>().Y + "," + waypoints.Last<Position>().Z + ", nextid: " + nextActionIndex);
                }
                else
                    log("Player hasn't moved. Holding...");

                await Task.Delay(1000);
            }
        }

        static public void Cancel() => Recording = false;

        static public void Save(string newActionListName)
        {
            nextactionIndexList[actionIndexList.Count - 1] = -1;
            string logwaypoints;
            string logactionIndex;
            string lognextActionIndex;
            string logstateStack;

            //Recording = false;
            string fp = "E:\\" + newActionListName;
            if (!File.Exists(fp))
            {
                FileStream stream = File.Create(fp);
                stream.Close();
                stream.Dispose();
            }
            for (int i = 0; i < waypoints.Count; i++)
            {
                Position pos = waypoints[i];
                logwaypoints = "waypoints.Add(new Position((float)" + pos.X + ", (float)" + pos.Y + ", (float)" + pos.Z + ", " + pos.Id + "));";
                logactionIndex = "actionIndexList.Add(" + actionIndexList[i] + ");";
                lognextActionIndex = "nextactionIndexList.Add(" + nextactionIndexList[i] + ");";
                logstateStack = "stateStack.Add(" + StackNames[i] + ");";
                using (StreamWriter writer = new StreamWriter(fp, true))
                {
                    writer.WriteLine("//Action " + i);
                    writer.WriteLine(logwaypoints);
                    writer.WriteLine(logactionIndex);
                    writer.WriteLine(lognextActionIndex);
                    writer.WriteLine(logstateStack);
                    writer.Close();
                }
            }
            //File.WriteAllText(fp, JsonConvert.SerializeObject(actionList));
        }


    }
}
