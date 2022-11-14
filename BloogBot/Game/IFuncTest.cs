using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloogBot.Game
{
    public interface IFuncTest
    {
        void FuncTest(int spellid, int spellcd, Action<string> log);

        void GetTargetEnt(Action<string> log);

        void TraverseObjects(Action<string> log, bool onUnits, bool onPlayers, bool onGameObjects, bool onItems);
    }
}
