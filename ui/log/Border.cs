using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class Border
    {
        public static readonly int TopLeft = 218;
        public static readonly int TopRight = 191;
        public static readonly int BottomLeft = 192;
        public static readonly int BottomRight = 217;
        public static readonly int Horizontal = 196;
        public static readonly int Vertical = 179;

        public static void Draw(SadConsole.Console console, Color color)
        {
            for (var x = 0; x < console.Width; x++)
            {
                for (var y = 0; y < console.Height; y++)
                {
                    DrawCell(x, y, color, console);
                }
            }
        }

        public static void DrawCell(int x, int y, Color borderColor, SadConsole.Console console)
        {
            if ( x == 0 && y == 0 )
            {
                console.SetGlyph (x, y, TopLeft, borderColor);
            }
            else if ( x == console.Width - 1 && y == 0)
            {
                console.SetGlyph (x, y, TopRight, borderColor);
            }
            else if ( x == 0 && y == console.Height - 1)
            {
                console.SetGlyph (x, y, BottomLeft, borderColor);
            }
            else if ( x == console.Width - 1 && y == console.Height - 1)
            {
                console.SetGlyph (x, y, BottomRight, borderColor);
            }
            else if (y == 0 || y == console.Height - 1)
            {
                console.SetGlyph (x, y, Horizontal, borderColor);
            }
            else if (x == 0 || x == console.Width - 1)
            {
                console.SetGlyph (x, y, Vertical, borderColor);
            }
        }
    }
}