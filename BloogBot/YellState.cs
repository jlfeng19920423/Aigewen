using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot.AI;

namespace BloogBot
{
    class YellState : IBotState
    {
        public void Update() => Console.WriteLine("Yell!!!!");
    }
}
