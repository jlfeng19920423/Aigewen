using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloogBot;
using BloogBot.Game;
using BloogBot.Game.Objects;
using BloogBot.Game.Enums;

namespace FuncTest
{
    [Export(typeof(IFuncTest))]
    public class Test : IFuncTest
    {
        public void FuncTest(int SpellId, Action<string> log)
        {
            try
            {
                LocalPlayer player = ObjectManager.Player;

                bool spellKnow = player.KnowsSpell(SpellId, false);

                log(SpellId + "spell known is " + spellKnow.ToString());

            }
            catch (Exception e)
            {
                //Logger.Log(e + "\n");
            }
        }

        public void GetTargetEnt(Action<string> log)
        {
            CGGuid lastTargetGuid;
            lastTargetGuid = MemoryManager.ReadGuid(IntPtr.Add(MemoryAddresses.MemBase, Offsets.LastTargetGuid));
            LocalPlayer player = ObjectManager.Player;
            WoWUnit Target = ObjectManager.Units.FirstOrDefault(u => u.Guid.isEqualTo(player.TargetGuid));
            WoWPlayer Players = ObjectManager.Players.FirstOrDefault(u => u.Guid.isEqualTo(player.TargetGuid));
            WoWUnit LastTarget = ObjectManager.Units.FirstOrDefault(u => u.Guid.isEqualTo(player.LastTargetGuid));
            log($"GameBase:0x{MemoryAddresses.MemBase.ToString("X2")}\t PlayerEntPtr:0x{player.EntPtr.ToString("X2")}\tPlayerGuid:{player.Guid.high}{player.Guid.low}");
            //Log($"CorpsePosition:({player.CorpsePosition.X},{player.CorpsePosition.Y},{player.CorpsePosition.Z})");

            if (Target != null)
            {
                log($"TargetEntPtr:0x{Target.EntPtr.ToString("X2")}\tPlayerEntPtr:0x{player.EntPtr.ToString("X2")}\tTargetGuid:{Target.Guid.high}{Target.Guid.low}");
                //Log($"Health:{Target.Health}\tMaxHealth:{Target.MaxHealth}\tLevel:{Target.Level}\t");
                //Log($"Name:{Target.Name}\tSex:{Target.Sex}\tRace:{Target.Race}\tTargetGuid:{Target.TargetGuid.high}{Target.TargetGuid.low}");
                //Log($"Position:{Target.UnitPosition.X},{Target.UnitPosition.Y},{Target.UnitPosition.Z}\tRotationD:{Target.RotationD}\tRotationF:{Target.RotationF}\tPitch:{Target.Pitch}");
                //Log($"Flag1:{Target.UnitFlag1}\tFlag2:{Target.UnitFlag2}\tFlag3:{Target.UnitFlag3}\tDynamicFlag:{Target.DynamicFlag}");
            }
            if (Players != null)
            {
                log($"PlayersEntPtr:0x{Players.EntPtr.ToString("X2")}\tPlayersGuid:{Players.Guid.high}{Players.Guid.low}\tPlayerName:{Players.Name}");
                //Log($"ChanId:{Players.ChanID}\t");
                //Log($"Name:{Players.Name}\tSex:{Players.Sex}\tRace:{Players.Race}\tTargetGuid:{Players.TargetGuid.high}{Players.TargetGuid.low}");
                //Log($"Position:{Players.UnitPosition.X},{Players.UnitPosition.Y},{Players.UnitPosition.Z}\tRotationD:{Players.RotationD}\tRotationF:{Players.RotationF}\tPitch:{Players.Pitch}");
                //Log($"Flag1:{Players.UnitFlag1}\tFlag2:{Players.UnitFlag2}\tFlag3:{Players.UnitFlag3}\tDynamicFlag:{Players.DynamicFlag}");
            }
            log($"LastTargetGuid:{lastTargetGuid.high}{lastTargetGuid.low}");
        }
    
        public void TraverseObjects(Action<string> log, bool onUnits, bool onPlayers, bool onGameObjects, bool onItems)
        {
            IEnumerable<WoWUnit> units = ObjectManager.Units;
            IEnumerable<WoWPlayer> players = ObjectManager.Players;
            IEnumerable<WoWGameObject> gameObjects = ObjectManager.GameObjects;
            IEnumerable<WoWItem> items = ObjectManager.Items;

            if (onUnits)
            {
                try
                {
                    log($"UnitsCount: {units.Count()}");
                    foreach (WoWUnit unit in units)
                    {
                        log($"UnitEntPtr:0x{unit.EntPtr.ToString("X2")}\tUnitGuid:{unit.Guid.high.ToString("X2")}{unit.Guid.low.ToString("X2")}\tUnitName:{unit.Name}");
                    }

                }
                catch (Exception e)
                {
                    //Logger.Log(e + "\n");
                }
            }

            if (onGameObjects)
            {
                try
                {
                    log($"GameObjectsCount: {gameObjects.Count()}");
                    foreach (WoWGameObject gameObject in gameObjects)
                    {
                        log($"GameObjectEntPtr:0x{gameObject.EntPtr.ToString("X2")}\tGameObjectGuid:{gameObject.Guid.high.ToString("X2")}{gameObject.Guid.low.ToString("X2")}\tGameObjectName:{gameObject.Name}");
                    }

                }
                catch (Exception e)
                {
                    //Logger.Log(e + "\n");
                }
            }

            if (onPlayers)
            {
                try
                {
                    log($"PlayersCount: {players.Count()}");
                    foreach (WoWPlayer player in players)
                    {
                        log($"PlayerEntPtr:0x{player.EntPtr.ToString("X2")}\tPlayerGuid:{player.Guid.high.ToString("X2")}{player.Guid.low.ToString("X2")}\tPlayerName:{player.Name}");
                    }

                }
                catch (Exception e)
                {
                    //Logger.Log(e + "\n");
                }
            }
        }
    }
}
