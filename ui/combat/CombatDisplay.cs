using Microsoft.Xna.Framework;
using Game.UI.Log;
using Game.Combat;
using System.Collections.Generic;

namespace Game.UI.Combat
{
    public class CombatDisplay : GridLayout
    {
        public Game.Combat.Combat Combat { get; }
        public GridLayout PositionsGrid { get; }
        public List<SadConsole.Console> HoverSurfaces { get; } = new List<SadConsole.Console>();
        public int FocusedEntityIndex { get; set; }

        public CombatDisplay(int width, int height, Game.Combat.Combat combat) : base(width, height, 2, 3)
        {
            Combat = combat;
            
            SetupLayout();

            PositionsGrid = Add((width, height) => new GridLayout(width, height, 8, 1, false), 0, 1);
            for(var i = 0; i < 8; i++)
            {
                var entity = combat.Combatants[i];
                var display = PositionsGrid.Add((width, height) => new PositionDisplay(entity, width, height), i, 0);

                var hoverSurface = new SadConsole.Console(display.Width, display.Height);
                hoverSurface.Position = display.Position;
                hoverSurface.Parent = PositionsGrid;
                var index = i;
                hoverSurface.MouseEnter += (setter, args) => EnteredEntityFocus(index);
                hoverSurface.MouseExit += (setter, args) => ExitedEntityFocus(index);
                HoverSurfaces.Add(hoverSurface);
            }

            

            Add((width, height) => new BorderedLayout(width, height)).Add((width, height) => new Button("test", () => {
                Combat.Log.AddLine(ColoredString.RecolorForeground(Color.Orange) + "Test test test test test" + ColoredString.Undo() + "test test test tes test");
            }));

            Add((width, height) => new BorderedLayout(width, height), 0, 2);
            Add((width, height) => new BorderedLayout(width, height), 1, 0, 1, 2)
                .Add((width, height) => new GravityLayout(width, height))
                    .Add((width, height) => new EntityDisplay(width, height, this), 2, true, LayoutGravity.CENTER, 2, true, LayoutGravity.CENTER);
            
            Add((width, height) => new LogDisplay(width, height, Combat.Log), 1, 2);
        }

        public void SetupLayout()
        {
            XSegments[0].Weight = 3;
            YSegments[0].Length = 5;
            YSegments[0].IsDynamic = false;
            CalculateDimensions();
        }

        public void EnteredEntityFocus(int index)
        {
            FocusedEntityIndex = index;
            DrawSelectionBox(index);
            UpdatedEntityFocus();
        }

        public void DrawSelectionBox(int index)
        {
            var surface = HoverSurfaces[index];
            var color = Color.Gold;
            surface.SetGlyph(0, surface.Height - 5, Border.TopLeft, color);
            surface.SetGlyph(0, surface.Height - 4, Border.Vertical, color);
            surface.SetGlyph(0, surface.Height - 3, Border.Vertical, color);
            surface.SetGlyph(0, surface.Height - 2, Border.BottomLeft, color);

            surface.SetGlyph(surface.Width - 1, surface.Height - 5, Border.TopRight, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 4, Border.Vertical, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 3, Border.Vertical, color);
            surface.SetGlyph(surface.Width - 1, surface.Height - 2, Border.BottomRight, color);
        }

        public void ExitedEntityFocus(int index)
        {
            HoverSurfaces[index].Clear();

            if(FocusedEntityIndex == index)
            {
                FocusedEntityIndex = -1;
                UpdatedEntityFocus();
            }
        }

        public void UpdatedEntityFocus()
        {
            Clear();
            var text = FocusedEntityIndex == -1 ? "null" : Combat.Combatants[FocusedEntityIndex].Name.ToString();
            UsePrintProcessor = true;
            Print(10, 1, text, Color.White);
        }
    }
}