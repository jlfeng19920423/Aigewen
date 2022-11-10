using BloogBot.Game;
using BloogBot.Game.Enums;
using BloogBot.Game.Objects;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BloogBot.AI.SharedStates
{
    public class FaceTo : IBotState
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

}
