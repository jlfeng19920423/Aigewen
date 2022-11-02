using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot.Game;

namespace BloogBot.AI
{
    public class ActionList
    {
        public readonly IList<Position> waypoints;
        public readonly IList<int> actionIndexList;
        public readonly IList<int> nextactionIndexList;

        public ActionList(IList<Position> waypoints, IList<int> actionIndexList, IList<int> nextactionIndexList)
        {
            this.waypoints = waypoints;
            this.actionIndexList = actionIndexList;
            this.nextactionIndexList = nextactionIndexList;
        }

        public int ActionIndex { get; set; }
        
    }
}
