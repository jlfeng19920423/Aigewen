using BloogBot.Game;
using BloogBot.Game.Enums;
using BloogBot.Game.Objects;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BloogBot.AI.SharedStates
{
    public class MoveToPositionState : IBotState
    {
        readonly Stack<IBotState> botStates;
        //readonly IDependencyContainer container;
        readonly bool use2DPop;
        readonly LocalPlayer player;
        readonly StuckHelper stuckHelper;
        readonly Position destination;
        IntPtr destinationPtr;
        readonly ActionList actionList;

        int stuckCount;

        //public MoveToPositionState(Stack<IBotState> botStates, IDependencyContainer container, Position destination, bool use2DPop = false)
        public MoveToPositionState(Stack<IBotState> botStates, ActionList actionList, bool use2DPop = false)
        {
            this.botStates = botStates;
            //this.container = container;
            //this.destination = destination;
            this.use2DPop = use2DPop; 
            this.actionList = actionList;
            this.destination = actionList.Waypoints.FirstOrDefault(u => u.Id == actionList.ActionIndex);
            player = ObjectManager.Player;
            //stuckHelper = new StuckHelper(botStates, container);
        }

        public void Update()
        {
            /*
            //var threat = container.FindThreat();
            
            if (threat != null)
            {
                //player.StopAllMovement();
                //botStates.Push(container.CreateMoveToTargetState(botStates, container, threat));
                return;
            }
            */
            float[] pos = { this.destination.X, this.destination.Y, this.destination.Z };
            destinationPtr = MemoryManager.GetPosAddr(ref pos);

            //Console.WriteLine("destination:{0},{1},{2}",destination.X,destination.Y,destination.Z);
            //if (stuckHelper.CheckIfStuck())
                //stuckCount++;

            if (use2DPop)
            {
                if (player.UnitPosition.DistanceTo2D(destination) < 3 || stuckCount > 20)
                {
                    //player.StopAllMovement();
                    botStates.Pop();
                    actionList.ActionIndex = actionList.NextActionIndexList.ElementAt(actionList.ActionIndex);
                    return;
                }
            }
            else
            {
                if (player.UnitPosition.DistanceTo(destination) < 3 || stuckCount > 20)
                {
                    //player.StopAllMovement();
                    botStates.Pop();
                    actionList.ActionIndex = actionList.NextActionIndexList.ElementAt(actionList.ActionIndex);
                    return;
                }
            }

            //var nextWaypoint = Navigation.GetNextWaypoint(ObjectManager.MapId, player.Position, destination, false);
            //player.MoveToward(nextWaypoint);
            if (this.destinationPtr != IntPtr.Zero) ThreadSynchronizer.RunOnMainThread(() => Functions.ClickToMoveMoveTo(player.EntPtr, this.destinationPtr));
            
            /*
            //actionList.ActionIndex = actionList.nextactionIndexList.ElementAt(actionList.ActionIndex);
            if (actionList.ActionIndex != -1)
            {
                //botStates.Push(currentStack(botStates, actionList));
                actionList.ActionIndex = actionList.nextactionIndexList.ElementAt(actionList.ActionIndex);
            }
            else
            {
                botStates.Pop();
                return;
            }
            */
        }
    }
}
