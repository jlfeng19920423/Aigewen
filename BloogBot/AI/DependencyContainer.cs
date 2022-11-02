using BloogBot.AI.SharedStates;
using BloogBot.Game;
using BloogBot.Game.Enums;
using BloogBot.Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloogBot.AI
{
    public class DependencyContainer : IDependencyContainer
    {
        static readonly string[] oozeNames = { "Acidic Swamp Ooze", "Black Slime", "Cloned Ectoplasm", "Cloned Ooze", "Corrosive Sap Beast", "Corrosive Swamp Ooze",
            "Cursed Ooze", "Devouring Ectoplasm", "Evolving Ectoplasm", "Gargantuan Ooze", "Glob of Viscidus", "Glutinous Ooze", "Green Sludge", "Irradiated Slime",
            "Jade Ooze", "Muculent Ooze", "Nightmare Ectoplasm", "Noxious Slime", "Plague Slime", "Primal Ooze", "Rotting Slime", "Sap Beast", "Silty Oozeling",
            "Tainted Ooze", "Vile Slime", "The Rot", "Viscidus", "The Ongar" };

        readonly Func<WoWUnit, bool> targetingCriteria;

        //IDictionary<string, PlayerTracker> PlayerTrackers { get; } = new Dictionary<string, PlayerTracker>();

        public DependencyContainer(
            Func<WoWUnit, bool> targetingCriteria,
            Func<Stack<IBotState>, IDependencyContainer, IBotState> createRestState,
            Func<Stack<IBotState>, IDependencyContainer, WoWUnit, IBotState> createMoveToTargetState,
            //Func<Stack<IBotState>, IDependencyContainer, WoWUnit, WoWPlayer, IBotState> createPowerlevelCombatState,
            //BotSettings botSettings,
            Probe probe)
            //IEnumerable<Hotspot> hotspots)
        {
            this.targetingCriteria = targetingCriteria;

            CreateRestState = createRestState;
            CreateMoveToTargetState = createMoveToTargetState;
            //CreatePowerlevelCombatState = createPowerlevelCombatState;
            //BotSettings = botSettings;
            Probe = probe;
            //Hotspots = hotspots;
        }

        public Func<Stack<IBotState>, IDependencyContainer, IBotState> CreateRestState { get; }

        public Func<Stack<IBotState>, IDependencyContainer, IBotState> CreateRetrieveCorpseState { get; }

        public Func<Stack<IBotState>, IDependencyContainer, IBotState> CreateMoveToCorpseState { get; }

        public Func<Stack<IBotState>, IDependencyContainer, WoWUnit, IBotState> CreateMoveToTargetState { get; }

        public Func<Stack<IBotState>, IDependencyContainer, WoWUnit, WoWPlayer, IBotState> CreatePowerlevelCombatState { get; }

        public BotSettings BotSettings { get; }

        public Probe Probe { get; }

        public IEnumerable<Hotspot> Hotspots { get; }

        public WoWUnit FindThreat()
        {
            return ObjectManager.Units.FirstOrDefault(u => u.TargetGuid.isEqualTo(ObjectManager.Player.Guid));
        }

        public WoWUnit FindClosestTarget()
        {
            return FindThreat() ??
                ObjectManager
                    .Units
                    .Where(u => u != null && u.Name != null && u.UnitPosition != null)
                    .Where(u => u.Health > 0)
                    //.Where(u => !u.TappedByOther)
                    //.Where(u => !u.IsPet)
                    //.Where(u => !Probe.BlacklistedMobIds.Contains(u.Guid))
                    //.Where(u => u.CreatureRank == CreatureRank.Normal || BotSettings.TargetingIncludedNames.Any(n => u.Name.Contains(n)))
                    //.Where(u => string.IsNullOrWhiteSpace(BotSettings.TargetingIncludedNames) || BotSettings.TargetingIncludedNames.Split('|').Any(m => u.Name.Contains(m)))
                    //.Where(u => string.IsNullOrWhiteSpace(BotSettings.TargetingExcludedNames) || !BotSettings.TargetingExcludedNames.Split('|').Any(m => u.Name.Contains(m)))
                    //.Where(u => BotSettings.CreatureTypes.Count == 0 || u.CreatureType == CreatureType.Mechanical || (u.CreatureType == CreatureType.Totem && u.Position.DistanceTo(ObjectManager.Player.Position) <= 20) || BotSettings.CreatureTypes.Contains(u.CreatureType.ToString()) || oozeNames.Contains(u.Name))
                    //.Where(u => BotSettings.UnitReactions.Count == 0 || BotSettings.UnitReactions.Contains(u.UnitReaction.ToString()))
                    //.Where(u => u.Level <= ObjectManager.Player.Level + BotSettings.LevelRangeMax && u.Level >= ObjectManager.Player.Level - BotSettings.LevelRangeMin)
                    //.Where(u => Navigation.CalculatePath(ObjectManager.MapId, ObjectManager.Player.Position, u.Position, false).Count() > 0)
                    // TODO: FactionId
                    // 71: Undercity, 85: Orgrimmar, 474: Gadgetzan
                    //.Where(u => u.FactionId != 71 && u.FactionId != 85 && u.FactionId != 474 && u.FactionId != 475 && u.FactionId != 1475)
                    //.Where(u => targetingCriteria(u))
                    //.OrderBy(u => Navigation.DistanceViaPath(ObjectManager.MapId, ObjectManager.Player.Position, u.Position))
                    .FirstOrDefault();
        }

        public Hotspot GetCurrentHotspot() => BotSettings.GrindingHotspot;

        public void CheckForTravelPath(Stack<IBotState> botStates, bool reverse, bool needsToRest = true)
        {
            /*
            var currentHotspot = BotSettings.GrindingHotspot;
            var travelPath = currentHotspot?.TravelPath;
            if (travelPath != null)
            {
                Position[] waypoints;
                if (reverse)
                    waypoints = travelPath.Waypoints.Reverse().ToArray();
                else
                    waypoints = travelPath.Waypoints;

                var closestTravelPathWaypoint = waypoints.OrderBy(l => l.DistanceTo(ObjectManager.Player.Position)).First();

                Position destination;
                if (reverse)
                    destination = waypoints.Last();
                else
                    destination = currentHotspot.Waypoints.OrderBy(l => l.DistanceTo(ObjectManager.Player.Position)).First();

                if (closestTravelPathWaypoint.DistanceTo(ObjectManager.Player.Position) < destination.DistanceTo(ObjectManager.Player.Position))
                {
                    var startingIndex = waypoints.ToList().IndexOf(closestTravelPathWaypoint);
                    botStates.Push(new TravelState(botStates, this, waypoints, startingIndex));
                    botStates.Push(new MoveToPositionState(botStates, this, closestTravelPathWaypoint));

                    if (reverse && needsToRest)
                        botStates.Push(CreateRestState(botStates, this));
                }
            }
            */
        }

        public bool RunningErrands { get; set; }

        int beepTimer;

        public bool UpdatePlayerTrackers()
        {
            var stopBot = false;

            try
            {
                
            }
            catch (Exception) { /* swallow it */ }

            return stopBot;
        }

        public bool DisableTeleportChecker { get; set; }
    }
}

