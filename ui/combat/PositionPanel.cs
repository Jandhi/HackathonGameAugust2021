using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Game.Audio.AudioManager;
using static Game.Util.ConsoleFunctions;

namespace Game.UI.Combat
{
    public class PositionPanel : GridLayout
    {
        public const int PositionDisplayGridWidth = 8;
        public const int PositionDisplayGridHeight = 2;

        public List<PositionDisplay> Positions { get; } = new List<PositionDisplay>();
        public List<SadConsole.Console> HoverSurfaces { get; } = new List<SadConsole.Console>();
        public Game.Combat.Combat Combat { get; }

        public PositionPanel(int width, int height, CombatDisplay combatDisplay) : base(width, height, PositionDisplayGridWidth, PositionDisplayGridHeight, false)
        {
            Combat = combatDisplay.Combat;
            SetupLayout();
            AddPositionDisplays(combatDisplay);
            combatDisplay.SelectedEntityIndex.StateChangeEvent += (obj, args) => {
                DrawSelectionBox(args.NewValue, combatDisplay.Theme.AccentColor);
            };

            var gravLayout = Add((width, height) => new GravityLayout(width, height), 0, 0, 8, 1);
            var text = ColoredString.Transform($"{Combat.Current.Get().Name}'s turn", s => s.ToUpper());
            gravLayout.Add((width, height) => new TextDisplay(text, width, height), ColoredString.GetLength(text), false, LayoutGravity.CENTER);
        }

        public void SetupLayout()
        {
            YSegments[0].IsDynamic = false;
            YSegments[0].Length = 1;
            CalculateDimensions();
        }

        private void AddPositionDisplays(CombatDisplay combatDisplay)
        {
            for(var i = 0; i < 8; i++)
            {
                var entity = combatDisplay.Combat.Combatants[i];

                var display = Add((width, height) => new PositionDisplay(entity, width, height, combatDisplay.Combat, combatDisplay.Theme), i, 1);
                Positions.Add(display);

                CreateHoverSurface(i, display, combatDisplay);
            }
        }

        private void CreateHoverSurface(int index, PositionDisplay display, CombatDisplay combatDisplay)
        {
            var hoverSurface = new SadConsole.Console(display.Width, display.Height);
            hoverSurface.Position = display.Position;
            hoverSurface.Parent = this;
            hoverSurface.MouseButtonClicked += (setter, args) => {
                var root = combatDisplay.AbilityPanel.CurrentAbility.Use(0, 0, Combat, Combat.Current, Combat.Combatants[4]);
                root.Do(true);

                var tap = Tap.CreateInstance();
                tap.Volume = 1;
                tap.Play();
            };
            hoverSurface.MouseEnter += (setter, args) => {
                combatDisplay.SelectedEntityIndex.Set(index);
                Click.CreateInstance().Play();
            };
            hoverSurface.MouseExit += (setter, args) => {
                hoverSurface.Clear();
                if(combatDisplay.SelectedEntityIndex == index)
                {
                    combatDisplay.SelectedEntityIndex.Set(-1);
                }

                if(Combat.Combatants[index] != null)
                {
                    display.HealthBar.IsHovered.State = false;
                }
            };
            hoverSurface.MouseMove += (setter, args) => {
                if(Combat.Combatants[index] != null)
                {
                    display.HealthBar.IsHovered.State = IsInConsole(args.MouseState.WorldCellPosition, display.HealthBar);
                }
            };
            HoverSurfaces.Add(hoverSurface);
        }

        private void DrawSelectionBox(int index, Color color)
        {
            if(index == -1 || Combat.Combatants[index] == null)
            {
                return;
            }

            var surface = HoverSurfaces[index];
            surface.SetGlyph(0, surface.Height - 5, Border.TopLeft, color);
            surface.SetGlyph(0, surface.Height - 4, Border.Vertical, color);
            surface.SetGlyph(0, surface.Height - 3, Border.Vertical, color);
            surface.SetGlyph(0, surface.Height - 2, Border.BottomLeft, color);

            surface.SetGlyph(surface.Width - 1, surface.Height - 5, Border.TopRight, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 4, Border.Vertical, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 3, Border.Vertical, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 2, Border.BottomRight, color);
        }
    }
}