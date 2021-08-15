using System.Collections.Generic;

namespace Game.Combat{
    public class Entity{

        public Dictionary<Stats, int> Stats { get; } = new Dictionary<Stats, int>();
        public Entity(Dictionary<Stats, int> stats){
            Stats = stats;
        }
    }
}
