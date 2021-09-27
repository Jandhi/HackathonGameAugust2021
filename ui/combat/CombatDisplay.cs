using Microsoft.Xna.Framework;
using Game.UI.Log;
using Game.Combat;
using System.Linq;
using System.Collections.Generic;
using static Game.Util.ConsoleFunctions;
using static Game.Audio.AudioManager;

namespace Game.UI.Combat
{
    public class CombatDisplay : GridLayout
    {

        public Game.Combat.Combat Combat { get; }
        // Middle Displays
        public GridLayout PositionsGrid { get; }
        public List<PositionDisplay> Positions { get; } = new List<PositionDisplay>();
        public List<SadConsole.Console> HoverSurfaces { get; } = new List<SadConsole.Console>();
        public int FocusedEntityIndex { get; set; }
        public int SelectedEntityIndex { get; set; }
        public Entity SelectedEntity => Combat.Combatants[SelectedEntityIndex];
        // Bottom Displays
        public GridLayout BottomLayout { get; set; }
        public AbilityDisplay AbilityDisplay { get; set; }
        public RadioGroup AbilityGroup { get; set; }
        // Side Display
        public EntityDisplay EntityDisplay { get; }
        
        public Theme Theme { get; }

        public CombatDisplay(int width, int height, Game.Combat.Combat combat, Theme theme = null) : base(width, height, 2, 3)
        {
            Combat = combat;
            Theme = theme ?? Theme.CurrentTheme;
            
            SetupLayout();

            PositionsGrid = Add((width, height) => new GridLayout(width, height, 8, 1, false), 0, 1);
            for(var i = 0; i < 8; i++)
            {
                var entity = combat.Combatants[i];

                var display = PositionsGrid.Add((width, height) => new PositionDisplay(entity, width, height), i, 0);
                Positions.Add(display);

                CreateHoverSurface(i, display);
            }

            Add((width, height) => new BorderedLayout(width, height)).Add((width, height) => new Button("test", () => {
                var entity = Combat.Combatants[3];
                var root = entity.Abilities[0].Use(0, 0, Combat, entity, Combat.Combatants[4]);
                root.Do(true);
            }));

            var bottom = Add((width, height) => new BorderedLayout(width, height), 0, 2).Add((width, height) => new GravityLayout(width, height));
            BottomLayout = bottom.Add((width, height) => new GridLayout(width, height, 2, 1), 2, true, LayoutGravity.CENTER, 2, true, LayoutGravity.CENTER);
            BottomLayout.XSegments[0].Weight = 1;
            BottomLayout.XSegments[1].Weight = 5;
            BottomLayout.CalculateDimensions();
            AbilityDisplay = BottomLayout.Add((width, height) => new AbilityDisplay(width, height, Theme), 1, 0);
            CreateAbilityGroup();
                

            EntityDisplay = Add((width, height) => new BorderedLayout(width, height), 1, 0, 1, 2)
                .Add((width, height) => new GravityLayout(width, height))
                    .Add((width, height) => new EntityDisplay(width, height), 2, true, LayoutGravity.CENTER, 2, true, LayoutGravity.CENTER);
            
            Add((width, height) => new LogDisplay(width, height, Combat.Log), 1, 2);

            DrawSelected();
        }

        public void SetupLayout()
        {
            XSegments[0].Weight = 3;
            YSegments[0].Length = 5;
            YSegments[0].IsDynamic = false;
            CalculateDimensions();
        }

        private void CreateHoverSurface(int index, PositionDisplay display)
        {
            var hoverSurface = new SadConsole.Console(display.Width, display.Height);
            hoverSurface.Position = display.Position;
            hoverSurface.Parent = PositionsGrid;
            hoverSurface.MouseButtonClicked += (setter, args) => {
                if(index != SelectedEntityIndex) 
                {
                    HoverSurfaces[SelectedEntityIndex].Clear();
                }
                SelectedEntityIndex = index;
                EntityDisplay.Entity = SelectedEntity;
                CreateAbilityGroup();
                DrawSelected();

                var tap = Tap.CreateInstance();
                tap.Volume = 1;
                tap.Play();
            };
            hoverSurface.MouseEnter += (setter, args) => {
                EnteredEntityFocus(index);
                Click.CreateInstance().Play();
            };
            hoverSurface.MouseExit += (setter, args) => {
                ExitedEntityFocus(index);
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

        private void CreateAbilityGroup()
        {
            if(AbilityGroup != null)
            {
                AbilityGroup.IsVisible = false;
                AbilityGroup = null;
            }

            if(SelectedEntity == null)
            {
                return;
            }

            AbilityGroup = BottomLayout.Add((width, height) => new RadioGroup(width, height, SelectedEntity.Abilities.Select(ability => ability.Name).ToList()));
            AbilityGroup.Selection.StateChangeEvent += (obj, args) => {
                AbilityDisplay.Ability = SelectedEntity.Abilities[args.Current];
            };

            AbilityDisplay.Ability = SelectedEntity.Abilities.Count != 0 ? SelectedEntity.Abilities[0] : null;
        }

        private void EnteredEntityFocus(int index)
        {
            FocusedEntityIndex = index;
            if(index != SelectedEntityIndex) {
                DrawSelectionBox(index, Theme.AccentColor);
            }
        }
        private void ExitedEntityFocus(int index)
        {
            HoverSurfaces[index].Clear();

            if(FocusedEntityIndex == index)
            {
                FocusedEntityIndex = -1;
            }

            if(SelectedEntityIndex == index)
            {
                DrawSelected();
            }
        }

        private void DrawSelected()
        {
            DrawSelectionBox(SelectedEntityIndex, Theme.MainColor);
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