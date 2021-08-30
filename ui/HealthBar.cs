using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;
using Game.Combat;

namespace Game.UI
{
    public class HealthBar : SadConsole.Console, IStatChangeListener
    {
        public static readonly Color HealthColor = Color.Green;
        public static readonly Color DamageColor = Color.Red;
        public Entity entity;
        public Entity Entity
        {
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

        private bool isHovered;
        public bool IsHovered {
            get 
            {
                return isHovered;
            }
            set
            {
                isHovered = value;
                Draw();
            } 
        }

        public TextDisplay TextDisplay { get; }

        public HealthBar(Entity entity, int width) : base(width, 1)
        {
            this.entity = entity;
            entity.Stats.Listeners.Add(this);

            TextDisplay = new TextDisplay("", width, 1, false);
            TextDisplay.Parent = this;
        }

        public void NotifyStatChange(Entity entity, Stat stat, float oldValue, float newValue)
        {
            if(Entity == entity)
            {
                Draw();
            }
        }

        public void Draw()
        {
            Clear();
            TextDisplay?.Clear();

            var maxHealth = entity.Stats[Stat.MaxHealth];
            var health = entity.Stats[Stat.Health];
            var greenTileCount = maxHealth == 0 ? 0 : (((Width) * health) / maxHealth);
            
            // Don't show empty if not dead
            if(greenTileCount == 0 && health > 0) 
            {
                greenTileCount = 1;
            }

            // Don't show full if not full
            if(greenTileCount == Width && health < maxHealth) 
            {
                greenTileCount = Width - 1;
            }

            for(var x = 0; x < Width; x++)
            {
                var color = x < greenTileCount ? HealthColor : DamageColor;
                SetGlyph(x, 0, 177, color);
            }

            if(isHovered)
            {
                var text = Entity.IsDead ? "DEAD" : $"{health}/{maxHealth}";
                var buffer = Width - text.Length;

                if(buffer < 0)
                {
                    TextDisplay.Text = "";
                }
                else
                {
                    TextDisplay.Text = text.PadLeft(buffer / 2 + text.Length, ' ');
                }
            }
        }
    }
}