using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Game.Audio.AudioManager;
using static Game.Util.ConsoleFunctions;

namespace Game.UI.Combat
{
    public class PositionPanel : GridLayout
    {
        public const int PositionDisplayGridWidth = 8;
        public const int PositionDisplayGridHeight = 1;

        public List<PositionDisplay> Positions { get; } = new List<PositionDisplay>();
        public List<SadConsole.Console> HoverSurfaces { get; } = new List<SadConsole.Console>();
        public Game.Combat.Combat Combat { get; }

        public PositionPanel(int width, int height, CombatDisplay combatDisplay) : base(width, height, PositionDisplayGridWidth, PositionDisplayGridHeight, false)
        {
            Combat = combatDisplay.Combat;
            AddPositionDisplays(combatDisplay);
        }

        private void AddPositionDisplays(CombatDisplay combatDisplay)
        {
            for(var i = 0; i < 8; i++)
            {
                var entity = combatDisplay.Combat.Combatants[i];

                var display = Add((width, height) => new PositionDisplay(entity, width, height), i, 0);
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
                

                var tap = Tap.CreateInstance();
                tap.Volume = 1;
                tap.Play();
            };
            hoverSurface.MouseEnter += (setter, args) => {
                combatDisplay.SelectedEntityIndex.Set(index);
                Click.CreateInstance().Play();
            };
            hoverSurface.MouseExit += (setter, args) => {
                if(combatDisplay.SelectedEntityIndex == index)
                {
                    combatDisplay.SelectedEntityIndex.Set(-1);
                }

                if(Combat.Combatants[index] != null)
                {
                    display.HealthBar.IsHovered = false;
                }
            };
            hoverSurface.MouseMove += (setter, args) => {
                if(Combat.Combatants[index] != null)
                {
                    display.HealthBar.IsHovered = IsInConsole(args.MouseState.WorldCellPosition, display.HealthBar);
                }
            };
            HoverSurfaces.Add(hoverSurface);
        }

        private void DrawSelectionBox(int index, Color color)
        {
            if(Combat.Combatants[index] == null)
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