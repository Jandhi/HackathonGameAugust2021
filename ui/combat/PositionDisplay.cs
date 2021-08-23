using Game.Combat;

namespace Game.UI.Combat
{
    public class PositionDisplay : GridLayout
    {
        public Entity Entity { get; }
        public HealthBar HealthBar { get; }

        public PositionDisplay(int width, int height) : base(width, height, 1, 5)
        {
            SetupLayout();

            var healthBarContainer = Add((width, height) => new GravityLayout(width, height), 0, 3);
            HealthBar = healthBarContainer.Add((width, height) => new HealthBar(10, 10, width), 2, true, LayoutGravity.CENTER);

            var nameContainer = Add((width, height) => new GravityLayout(width, height), 0, 2);
            var name = nameContainer.Add((width, height) => new SadConsole.Console(width, height), 2, true, LayoutGravity.CENTER);
            name.Print(0, 0, "name");
        }

        public void SetupLayout()
        {
            YSegments[1].IsDynamic = false;
            YSegments[1].Length = 1;

            YSegments[2].IsDynamic = false;
            YSegments[2].Length = 1;

            YSegments[3].IsDynamic = false;
            YSegments[3].Length = 1;

            YSegments[4].IsDynamic = false;
            YSegments[4].Length = 2;

            CalculateDimensions();
        }
    } 
}