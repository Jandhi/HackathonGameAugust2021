using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class BorderedButton : Button
    {
        public BorderedButton(string text, Action action) : base(text, action, text.Length + 2, 3)
        {
            Draw();
        }

        public override void Draw()
        {
            Print(1, 1, Text, Theme.TextColor);

            var borderColor = IsHovered ? Theme.HoveredColor : Theme.AccentColor;

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    DrawCell(x, y, borderColor);
                }
            }
        }

        public void DrawCell(int x, int y, Color borderColor)
        {
            if ( x == 0 && y == 0 )
                    {
                        SetGlyph (x, y, 218, borderColor);
                    }
                    else if ( x == Width - 1 && y == 0)
                    {
                        SetGlyph (x, y, 191, borderColor);
                    }
                    else if ( x == 0 && y == Height - 1)
                    {
                        SetGlyph (x, y, 192, borderColor);
                    }
                    else if ( x == Width - 1 && y == Height - 1)
                    {
                        SetGlyph (x, y, 217, borderColor);
                    }
                    else if (y == 0 || y == Height - 1)
                    {
                        SetGlyph (x, y, 196, borderColor);
                    }
                    else if (x == 0 || x == Width - 1)
                    {
                        SetGlyph (x, y, 179, borderColor);
                    }
        }
    }
}