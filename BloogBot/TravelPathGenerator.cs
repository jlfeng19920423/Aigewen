using BloogBot.Game;
using BloogBot.Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloogBot
{
    static public class TravelPathGenerator
    {
        // TODO: this is wrong. we need a better way to notify the UI.
        static Action callback;

        static Position previousPosition;
        static readonly IList<Position> positions = new List<Position>();

        static public void Initialize(Action parCallback)
        {
            callback = parCallback;
        }

        static public bool Recording { get; private set; }

        static public int PositionCount => positions.Count;

        static public void StartRecord(WoWPlayer player)
        {
            Recording = true;
            positions.Clear();
        }

        static public void Record(WoWPlayer player, Action<string> log)
        {
            if (Recording)
            {
                Position position = new Position(player.UnitPosition.X, player.UnitPosition.Y, player.UnitPosition.Z, PositionCount);
                positions.Add(position);
                previousPosition = player.UnitPosition;
                log("Adding waypoint " + positions.Last<Position>().X+","+positions.Last<Position>().Y + ","+ positions.Last<Position>().Z);
                callback();
            }
        }

        static public void Erase(WoWPlayer player, Action<string> log)
        {
            if (Recording)
            {
                if (positions.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    positions.RemoveAt(positions.Count - 1);
                    log("erase last waypoint");
                    callback();
                }
            }
        }

        static public void ListPath(WoWPlayer player, Action<string> log)
        {
            if (Recording)
            {
                if (positions.Any()) //prevent IndexOutOfRangeException for empty list
                {
                    var count = positions.Count;
                    foreach (var position in positions)
                    {
                        log($"{position.Id}:{position.X}\t{position.Y}\t{position.Z}");
                    }
                    callback();
                }
            }
        }

        static public void Cancel() => Recording = false;

        static public Position[] Save()
        {
            Recording = false;
            return positions.ToArray();
        }
    }
}
