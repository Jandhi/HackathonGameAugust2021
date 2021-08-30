using SadConsole.Input;
using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class HealthBar : SadConsole.Console
    {
        public static readonly Color HEALTH = Color.Green;
        public static readonly Color DAMAGE = Color.Red;

        private int maxHealth;
        public int MaxHealth { 
            get 
            {
                return maxHealth;
            } 
            set 
            {
                maxHealth = value;
                Draw();
            }
        }
        private int health;
        public int Health { 
            get 
            {
                return health;
            } 
            set 
            {
                health = value;
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

        public HealthBar(int health, int maxHealth, int width) : base(width, 1)
        {
            MaxHealth = maxHealth;
            Health = health;

            TextDisplay = new TextDisplay("", width, 1, false);
            TextDisplay.Parent = this;
        }

        public void Draw()
        {
            Clear();
            TextDisplay?.Clear();

            var greenTileCount = MaxHealth == 0 ? 0 : ((Width) * Health) / MaxHealth;
            
            // Don't show empty if not dead
            if(greenTileCount == 0 && Health > 0) 
            {
                greenTileCount = 1;
            }

            // Don't show full if not full
            if(greenTileCount == Width && Health < MaxHealth) 
            {
                greenTileCount = Width - 1;
            }

            for(var x = 0; x < Width; x++)
            {
                var color = x < greenTileCount ? HEALTH : DAMAGE;
                SetGlyph(x, 0, 177, color);
            }

            if(isHovered)
            {
                var hpText = $"{health}/{maxHealth}";
                var buffer = Width - hpText.Length;

                if(buffer < 0)
                {
                    TextDisplay.Text = "";
                }
                else
                {
                    TextDisplay.Text = hpText.PadLeft(buffer / 2 + hpText.Length, ' ');
                }
            }
        }
    }
}