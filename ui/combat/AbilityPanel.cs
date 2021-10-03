using Game.Combat.Ability;
using Game.Combat;
using System.Linq;

namespace Game.UI.Combat
{
    public class AbilityPanel : GridLayout
    {
        public static readonly int AbilityDisplayGridWidth = 2;
        public static readonly int AbilityDisplayGridHeight = 1;
    
        public RadioGroup AbilitySelection { get; set; }
        public SadConsole.Console AbilityDisplay { get; set; }
        public Entity Current { get; set; }
        public Ability CurrentAbility => Current.Abilities[AbilitySelection.Selection];

        public Theme Theme { get; }
        public AbilityPanel(int width, int height, Game.Combat.Combat combat, Theme theme = null) : base(width, height, AbilityDisplayGridWidth, AbilityDisplayGridHeight)
        {
            Current = combat.Current;
            Theme = theme ?? Theme.CurrentTheme;
            UsePrintProcessor = true;

            // Set up 1:5 ratio
            XSegments[0].Weight = 1;
            XSegments[1].Weight = 5;
            CalculateDimensions();

            AddSubconsoles(combat.Current);

            combat.Current.StateChangeEvent += (obj, args) => {
                AbilitySelection.Items.Set(args.Current.Abilities.Select(ability => ability.Name).ToList());
                Current = args.Current;
            };

            AbilitySelection.Selection.StateChangeEvent += (obj, args) => {
                DrawAbilityDisplay();
            };

            DrawAbilityDisplay();
        }

        private void AddSubconsoles(Game.Combat.Entity currentEntity)
        {
            AbilitySelection = Add((width, height) => new RadioGroup(width, height, currentEntity.Abilities.Select(ability => ability.Name).ToList()));
            AbilityDisplay = Add((width, height) => new SadConsole.Console(width, height), 1);
            AbilityDisplay.UsePrintProcessor = true;
        }

        private void DrawAbilityDisplay()
        {
            AbilityDisplay.Clear();
            for(int y = 0; y < Height; y++)
            {
                AbilityDisplay.Print(0, y, "|", Theme.AccentColor);
            }
            AbilityDisplay.Print(3, 0, ColoredString.Transform(CurrentAbility.Name, s => s.ToUpper()).ToString());
        }
    }
}