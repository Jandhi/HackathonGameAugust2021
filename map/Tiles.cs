using Microsoft.Xna.Framework;
using static Game.UI.ColorStrings;
using Game.Combat;

namespace Game.Map
{
    public class Tile
    {
        public virtual string Look { get; } = RecolorForeground(Color.Gray) + "." + Undo();
        public Entity Entity { get; set; }

        public bool IsTraversable { 
            get 
            {
                return Entity == null;
            }
        }
    }
}