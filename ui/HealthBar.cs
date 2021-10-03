using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;
using Game.Combat;
using Game.Util;

namespace Game.UI
{
    public class HealthBar : SadConsole.Console
    {
        public static readonly Color HealthColor = Color.Green;
        public static readonly Color DamageColor = Color.Red;
        public VariableContainer<Entity> Entity;

        public VariableContainer<bool> IsHovered { get; } = new VariableContainer<bool>(false);

        public TextDisplay TextDisplay { get; }

        public HealthBar(VariableContainer<Entity> entity, int width) : base(width, 1)
        {
            Entity = entity;

            TextDisplay = new TextDisplay("", width, 1, false);
            TextDisplay.Parent = this;

            Entity.StateChangeEvent += (obj, args) => 
            {
                AddStatChangeListener(args.NewValue);
                Draw();
            };

            IsHovered.StateChangeEvent += (obj, args) =>
            {
                Draw();
            };

            AddStatChangeListener(entity);
            Draw();
        }

        private void AddStatChangeListener(Entity entity)
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
            TextDisplay?.Clear();

            if(Entity.Get() == null)
            {
                return;
            }

            var maxHealth = Entity.Get().Stats[Stat.MaxHealth];
            var health = Entity.Get().Stats[Stat.Health];
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

            if(IsHovered)
            {
                var text = Entity.Get().IsDead ? "DEAD" : $"{health}/{maxHealth}";
                var buffer = Width - text.Length;

                if(buffer < 0)
                {
                    TextDisplay.Text.Set("");
                }
                else
                {
                    TextDisplay.Text.Set(text.PadLeft(buffer / 2 + text.Length, ' '));
                }
            }
        }
    }
}