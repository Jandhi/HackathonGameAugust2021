using Microsoft.Xna.Framework;
using Game.Combat;

namespace Game.UI.Combat
{
    public class EntityDisplay : GridLayout, IUIElement
    {
        public static readonly int EntityDisplayGridWidth = 1;
        public static readonly int EntityDisplayGridHeight = 5;
        public Entity entity;
        public Entity Entity { 
            get
            {
                return entity;
            } 
            set 
            {
                entity = value;
                Draw();
            } 
        }
        public Theme Theme { get; }
        public EntityDisplay(int width, int height, Theme theme = null) : base(width, height, EntityDisplayGridWidth, EntityDisplayGridHeight)
        {
            SetupLayout();
            Theme = theme ?? Theme.CurrentTheme;
            UsePrintProcessor = true;

        }

        public void Draw()
        {
            Clear();

            if(Entity != null)
            {
                var line = 0;

                Print(0, line, Entity.Name.ToString()); 
                line++;
                
                Print(0, line, new ColoredString("---------", Theme.AccentColor).ToString());
                line++;

                var tags = "";
                for(var i = 0; i < Entity.Tags.Count; i++)
                {
                    tags += Entity.Tags[i].Name.ToString();
                    if(i < Entity.Tags.Count - 1)
                    {
                        tags += ", ";
                    }
                }
                Print(0, line, tags);
                line++;

                var health = Entity.Stats[Stat.Health];
                var max = Entity.Stats[Stat.MaxHealth];
                Print(0, line, $"hp: {health}/{max}");
                line++;

            }
        }

        public void SetupLayout()
        {
            YSegments[0].Length = 1;
            YSegments[0].IsDynamic = false;


            CalculateDimensions();
        }
    }
}