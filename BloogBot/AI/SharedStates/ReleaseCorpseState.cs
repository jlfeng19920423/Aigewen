using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot.Game;

namespace BloogBot.AI.SharedStates
{
    public class ReleaseCorpseState : IBotState
    {
        readonly Stack<IBotState> botStates;
        readonly IDependencyContainer container;

        public ReleaseCorpseState(Stack<IBotState> botStates, IDependencyContainer container)
        {
            this.botStates = botStates;
            this.container = container;
        }
        public void Update()
        {
            if (Wait.For("StartReleaseCorpseStateDelay", 1000))
            {
                if (!ObjectManager.Player.IsGhost)
                    Functions.ReleaseCorpse(ObjectManager.Player.EntPtr);
                else
                {
                    if (Wait.For("LeaveReleaseCorpseStateDelay", 2000))
                    {
                        botStates.Pop();
                        return;
                    }
                }
            }
        }
    }
}
