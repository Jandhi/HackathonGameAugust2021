using System.Collections.Generic;

namespace Game.Combat
{
    public class Player : Entity
    {
        public override string Look => "@";

        public Player(string name, Dictionary<Stat,int> stats) : base(name, stats)
        {
            
        }
    }
}