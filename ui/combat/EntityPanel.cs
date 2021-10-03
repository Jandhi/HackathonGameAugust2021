using Microsoft.Xna.Framework;
using Game.Combat;
using Game.Util;

namespace Game.UI.Combat
{
    public class EntityPanel : GridLayout, IUIElement
    {
        public static readonly int EntityDisplayGridWidth = 1;
        public static readonly int EntityDisplayGridHeight = 5;
        public VariableContainer<Entity> Entity { get; }
        public Theme Theme { get; }
        public EntityPanel(int width, int height, VariableContainer<Entity> entity, Theme theme = null) : base(width, height, EntityDisplayGridWidth, EntityDisplayGridHeight)
        {
            Entity = entity;
            SetupLayout();
            Theme = theme ?? Theme.CurrentTheme;
            UsePrintProcessor = true;

            Entity.StateChangeEvent += (obj, args) => {
                SetupListener(args.NewValue);
                Draw();
            };

            SetupListener(entity);
            Draw();
        }

        private void SetupListener(Entity entity)
        {
            if(entity == null)
            {
                return;
            }

            entity.Stats.StatChangeEvent += (obj, args) => 
            {
                Draw();
            };
        }

        public void Draw()
        {
            Clear();

            if(Entity.Get() != null)
            {
                var line = 0;

                Print(0, line, Entity.Get().Name.ToString()); 
                line++;
                
                Print(0, line, new ColoredString("---------", Theme.AccentColor).ToString());
                line++;

                var tags = "";
                for(var i = 0; i < Entity.Get().Tags.Count; i++)
                {
                    tags += Entity.Get().Tags[i].Name.ToString();
                    if(i < Entity.Get().Tags.Count - 1)
                    {
                        tags += ", ";
                    }
                }
                Print(0, line, tags);
                line++;

                var health = Entity.Get().Stats[Stat.Health];
                var max = Entity.Get().Stats[Stat.MaxHealth];
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