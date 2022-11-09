using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot.Game;
using Newtonsoft.Json;

namespace BloogBot.AI
{
    [Serializable()]
    public class ActionList
    {
        public readonly List<Position> Waypoints;
        public readonly List<int> ActionIndexList;
        public readonly List<int> NextActionIndexList;
        public readonly List<Func<Stack<IBotState>, ActionList, IBotState>> StateStack;

        public ActionList(List<Position> waypoints, List<int> actionIndexList, List<int> nextActionIndexList, List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack)
        {
            this.Waypoints = waypoints;
            this.ActionIndexList = actionIndexList;
            this.NextActionIndexList = nextActionIndexList;
            this.StateStack = stateStack;
        }
        public int ActionIndex { get; set; }
        
    }
}
