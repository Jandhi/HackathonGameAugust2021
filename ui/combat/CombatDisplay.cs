using Microsoft.Xna.Framework;
using Game.UI.Log;
using Game.Combat;
using Game.Util;
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
        public PositionPanel PositionPanel { get; }
        public List<PositionDisplay> Positions { get; } = new List<PositionDisplay>();
        public VariableContainer<int> SelectedEntityIndex { get; set; }
        public LinkedContainer<Entity, int> SelectedEntity { get; }
        // Bottom Displays
        public GridLayout BottomLayout { get; set; }
        public AbilityPanel AbilityDisplay { get; set; }
        public RadioGroup AbilityGroup { get; set; }
        // Side Display
        public EntityPanel EntityPanel { get; }
        
        public Theme Theme { get; }

        public CombatDisplay(int width, int height, Game.Combat.Combat combat, Theme theme = null) : base(width, height, 2, 3)
        {
            Combat = combat;
            SelectedEntityIndex = new VariableContainer<int>(0);
            SelectedEntity = new LinkedContainer<Entity, int>(SelectedEntityIndex, index => index == -1 ? null : combat.Combatants[index]);
            Theme = theme ?? Theme.CurrentTheme;
            
            SetupLayout();

            PositionPanel = Add((width, height) => new BorderedLayout(width, height), 0, 1).Add((width, height) =>  new PositionPanel(width, height, this));

            Add((width, height) => new BorderedLayout(width, height)).Add((width, height) => new Button("test", () => {
                var entity = Combat.Combatants[3];
                var root = entity.Abilities[0].Use(0, 0, Combat, entity, Combat.Combatants[4]);
                root.Do(true);
            }));

            var bottom = Add((width, height) => new BorderedLayout(width, height), 0, 2).Add((width, height) => new GravityLayout(width, height));
            BottomLayout = bottom.Add((width, height) => new AbilityPanel(width, height, Combat), 2, true, LayoutGravity.CENTER, 2, true, LayoutGravity.CENTER);
                

            EntityPanel = Add((width, height) => new BorderedLayout(width, height), 1, 0, 1, 2)
                .Add((width, height) => new GravityLayout(width, height))
                    .Add((width, height) => new EntityPanel(width, height, SelectedEntity), 2, true, LayoutGravity.CENTER, 2, true, LayoutGravity.CENTER);
            
            Add((width, height) => new LogDisplay(width, height, Combat.Log), 1, 2);
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