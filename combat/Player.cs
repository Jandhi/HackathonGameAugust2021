using System.Collections.Generic;
using Game.UI;

namespace Game.Combat
{
    public class Player : Entity
    {
        public override string Look => "@";

        public Player(ColoredString name, Dictionary<Stat,float> stats) : base(name, stats)
        {
            
        }
    }
}