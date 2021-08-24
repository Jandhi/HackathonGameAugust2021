using Game.Combat;

namespace Game.UI.Combat
{
    public class PositionDisplay : GridLayout
    {
        public Entity Entity { get; }
        public HealthBar HealthBar { get; }

        public PositionDisplay(Entity entity, int width, int height) : base(width, height, 1, 5)
        {
            Entity = entity;

            SetupLayout();

            var healthBarContainer = Add((width, height) => new GravityLayout<HealthBar>(width, height), 0, 3);
            HealthBar = healthBarContainer.Add((width, height) => new HealthBar(10, 10, width), 2, true, LayoutGravity.CENTER);

            var nameContainer = Add((width, height) => new GravityLayout<SadConsole.Console>(width, height), 0, 2);
            var name = nameContainer.Add((width, height) => new TextDisplay(entity.Name.ToString(), width, height), 2, true, LayoutGravity.CENTER);
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