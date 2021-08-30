using Game.Combat;
using SadConsole.Input;
using System;

namespace Game.UI.Combat
{
    public class PositionDisplay : GridLayout
    {
        public static readonly int PositionDisplayGridWidth = 1;
        public static readonly int PositionDisplayGridHeight = 5;

        public Entity entity;
        public Entity Entity { 
            get
            {
                return entity;
            } 
            set
            {
                entity = value;
                UpdateHealthBar();
            } 
        }
        public HealthBar HealthBar { get; }
        public void UpdateHealthBar()
        {
            HealthBar.Health = (int) entity.Stats[Stat.Health];
            HealthBar.MaxHealth = (int) entity.Stats[Stat.MaxHealth];
        }

        public PositionDisplay(Entity entity, int width, int height) : base(width, height, PositionDisplayGridWidth, PositionDisplayGridHeight)
        {

            SetupLayout();

            var healthBarContainer = Add((width, height) => new GravityLayout<HealthBar>(width, height), 0, 3);
            HealthBar = healthBarContainer.Add((width, height) => new HealthBar(10, 10, width), 2, true, LayoutGravity.CENTER);

            Entity = entity;

            var nameContainer = Add((width, height) => new GravityLayout<TextDisplay>(width, height), 0, 2);
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