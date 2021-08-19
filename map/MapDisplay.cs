using Microsoft.Xna.Framework;
using System.Linq;

namespace Game.Map
{
    public class MapDisplay : SadConsole.Console, Game.UI.IDrawable
    {
        public Map Map { get; }
        public Point ScrollPosition { get; } = new Point(0, 0);

        public MapDisplay(int width, int height, Map map) : base(width, height)
        {
            Map = map;
            UsePrintProcessor = true;
            Draw();
        }

        public void Draw()
        {
            foreach(var x in Enumerable.Range(0, Map.Width - ScrollPosition.X))
            {
                foreach(var y in Enumerable.Range(0, Map.Height - ScrollPosition.Y))
                {
                    var mapX = x + ScrollPosition.X;
                    var mapY = y + ScrollPosition.Y;
                    if(mapX >= 0 && mapX < Map.Width && mapY >= 0 && mapY < Map.Height)
                    {
                        Print(x, y, Map.Tiles[mapX,mapY].Look);
                    }
                }
            }
        }
    }
}