using Game.Combat.Ability;
using Game.UI;

namespace Game.UI.Combat
{
    public class AbilityDisplay : GridLayout, IUIElement
    {
        public static readonly int AbilityDisplayGridWidth = 1;
        public static readonly int AbilityDisplayGridHeight = 1;
        private Ability ability;
        public Ability Ability 
        { 
            get
            {
                return ability;
            } 
            set
            {
                ability = value;
                Draw();
            } 
        }

        public Theme Theme { get; }
        public AbilityDisplay(int width, int height, Theme theme = null) : base(width, height, AbilityDisplayGridWidth, AbilityDisplayGridHeight)
        {
            Theme = theme ?? Theme.CurrentTheme;
            UsePrintProcessor = true;
        }

        public void Draw()
        {
            Clear();

            for(int y = 0; y < Height; y++)
            {
                Print(0, y, "|", Theme.AccentColor);
            }

            if(Ability == null) // nothing to draw
            {
                return;
            }
            
            Print(3, 0, ColoredString.From(Ability.Name).Transform(s => s.ToUpper()).ToString());
        }
    }
}