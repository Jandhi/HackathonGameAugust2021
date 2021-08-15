using System.Collections.Generic;

namespace Game.Combat
{
    public class Player : Entity
    {
        public Player(string name, Dictionary<Stats,int> stats) : base(name, stats)
        {
            
        }
    }
}