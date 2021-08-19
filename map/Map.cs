using System.Linq;

namespace Game.Map
{
    public class Map
    {
        public int Width { get; }
        public int Height { get; }
        public Tile[,] Tiles { get; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            Tiles = new Tile[width, height];

            for(var x = 0; x < width; x++)
            {

                for(var y = 0; y < height; y++)
                {
                    Tiles[x,y] = new Tile();
                }
            }
        }
    }
}