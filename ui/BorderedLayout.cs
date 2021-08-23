using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class BorderedLayout : BorderedLayout<SadConsole.Console>
    {
        public BorderedLayout(int width, int height, Theme theme = null) : base(width, height, theme)
        {
        }
    }

    public class BorderedLayout<T> : SadConsole.Console, IUIElement where T : SadConsole.Console
    {
        public T Containee { get; set; }
        public Theme Theme { get; }

        public BorderedLayout(int width, int height, Theme theme = null) : base(width, height)
        {
            Theme = theme ?? new Theme();
            Draw();
        }

        public TAdded Add<TAdded>(Func<int, int, TAdded> consoleConstructor) where TAdded : T
        {
            var containee = consoleConstructor(Width - 2, Height - 2);
            containee.Position = new Point(1, 1);
            containee.Parent = this;
            Containee = containee;
            return containee;
        }

        public void Draw()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    DrawCell(x, y, Theme.AccentColor);
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