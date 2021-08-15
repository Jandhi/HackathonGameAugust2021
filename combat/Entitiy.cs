using Systems.Collections.Generic;

namespace Game.Combat{
    public class Entity{

        public Dictionary<Stats, int> EntityStats{get;} = new Dictionary<Stats, int>();
        public Entity(Dictionary<Stats, int> the_stats){
            EntityStats = the_stats;
        }
    }
}
