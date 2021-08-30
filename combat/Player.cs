using System.Collections.Generic;
using Game.UI;

namespace Game.Combat
{
    public class Player : Entity
    {
        public override string Look => "@";

        public Player(ColoredString name, StatBlock stats) : base(name, stats)
        {
            
        }
    }
}