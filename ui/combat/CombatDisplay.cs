using Microsoft.Xna.Framework;
using Game.Combat;

namespace Game.UI.Combat
{
    public class CombatDisplay : GridLayout
    {
        public GridLayout PositionsGrid { get; }

        public CombatDisplay(int width, int height, Game.Combat.Combat combat) : base(width, height, 2, 3)
        {
            SetupLayout();

            PositionsGrid = Add((width, height) => new GridLayout(width, height, 8, 1, false), 0, 1);
            for(var i = 0; i < 8; i++)
            {
                PositionsGrid.Add((width, height) => new PositionDisplay(width, height), i, 0);
            }

            Add((width, height) => new SadConsole.Console(width, height)).Fill(Color.Blue, Color.Blue, 0);
            Add((width, height) => new SadConsole.Console(width, height), 0, 2).Fill(Color.Yellow, Color.Yellow, 0);
            Add((width, height) => new SadConsole.Console(width, height), 1, 0, 1, 3).Fill(Color.Gray, Color.Gray, 0);

            
        }

        public void SetupLayout()
        {
            XSegments[0].Weight = 3;
            YSegments[0].Length = 5;
            YSegments[0].IsDynamic = false;
            CalculateDimensions();
        }
    }
}