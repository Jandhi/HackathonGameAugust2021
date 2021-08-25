using Microsoft.Xna.Framework;

namespace Game.UI.Combat
{
    public class EntityDisplay : GridLayout, IDrawable
    {
        public static readonly int EntityDisplayGridWidth = 1;
        public static readonly int EntityDisplayGridHeight = 5;

        public CombatDisplay CombatDisplay { get; }
        public EntityDisplay(int width, int height, CombatDisplay combatDisplay) : base(width, height, EntityDisplayGridWidth, EntityDisplayGridHeight)
        {
            CombatDisplay = combatDisplay;
            SetupLayout();

            
        }

        public void Draw()
        {

        }

        public void SetupLayout()
        {
            YSegments[0].Length = 1;
            YSegments[0].IsDynamic = false;


            CalculateDimensions();
        }
    }
}