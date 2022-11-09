using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloogBot.AI.SharedStates
{
    public class ActionStackState : IBotState
    {
        readonly Stack<IBotState> botStates;
        //readonly IDependencyContainer container;
        //readonly List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack;
        readonly ActionList actionList;


        public ActionStackState(Stack<IBotState> botStates, ActionList actionList)
        {
            this.botStates = botStates;
            //this.container = container;
            this.actionList = actionList;
        }
        public void Update()
        {

            Func<Stack<IBotState>, ActionList, IBotState> currentStack = actionList.StateStack.ElementAt(actionList.ActionIndex);

            //botStates.Push(currentStack(botStates, actionIndex, actionList));
            if (actionList.ActionIndex != -1)
            {
                botStates.Push(currentStack(botStates, actionList));
                //actionList.ActionIndex = actionList.nextactionIndexList.ElementAt(actionList.ActionIndex);
            }
            else 
            {
                botStates.Pop();
                return;
            }
            //Console.WriteLine("ActionStackState");
            //tell the action based on the action Index

            //push action state, including the actionList and action Index

            // in every state, we should update the  the action Index (next Index or jump to another index)
        }
    }
}
