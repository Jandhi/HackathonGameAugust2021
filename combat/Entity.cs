using System.Collections.Generic;

namespace Game.Combat{
    public class Entity{

        public string Name { get; }
        public Dictionary<Stats, int> Stats { get; } = new Dictionary<Stats, int>();

        public Entity(string name, Dictionary<Stats, int> stats){
            Name = name;
            Stats = stats;
        }
    }
}
