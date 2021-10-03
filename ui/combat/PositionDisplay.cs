using Game.Combat;
using Game.Util;
using SadConsole.Input;
using System;

namespace Game.UI.Combat
{
    public class PositionDisplay : GridLayout, IUIElement
    {
        public static readonly int PositionDisplayGridWidth = 1;
        public static readonly int PositionDisplayGridHeight = 5;

        public VariableContainer<Entity> Entity;
        public HealthBar HealthBar { get; }
        public Theme Theme { get; }
        public Game.Combat.Combat Combat { get; }

        public PositionDisplay(Entity entity, int width, int height, Game.Combat.Combat combat, Theme theme = null) : base(width, height, PositionDisplayGridWidth, PositionDisplayGridHeight)
        {
            Combat = combat;
            Theme = theme ?? Theme.CurrentTheme;
        
            SetupLayout();
            

            Entity = new VariableContainer<Entity>(entity);

            var nameContainer = Add((width, height) => new GravityLayout<TextDisplay>(width, height), 0, 2);
            var name = nameContainer.Add((width, height) => new TextDisplay(entity == null ? "" : entity.Name, width, height), 2, true, LayoutGravity.CENTER);

            var healthBarContainer = Add((width, height) => new GravityLayout<HealthBar>(width, height), 0, 3);
            HealthBar = healthBarContainer.Add((width, height) => new HealthBar(Entity, width), 2, true, LayoutGravity.CENTER);

            Entity.StateChangeEvent += (obj, args) => 
            {
                name.Text.Set(args.NewValue.Name);
            };

            Combat.Current.StateChangeEvent += (obj, args) =>
            {
                Draw();
            };

            Draw();
        }

        public void Draw()
        {
            Clear();

            if(Combat.Current.Get() == Entity.Get())
            {
                for(int x = 0; x < Width; x++)
                {
                    var value = Border.Horizontal;

                    if(x == 0)
                    {
                        value = Border.BottomLeft;
                    }
                    else if(x == Width - 1)
                    {
                        value = Border.BottomRight;
                    }

                    SetGlyph(x, Height - 2, value, Theme.MainColor);                   
                }
            }
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