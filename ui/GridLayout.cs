using SadConsole;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class GridLayoutSegment
    {
        public bool IsDynamic { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
    }

    public class GridLayout : SadConsole.Console
    {
        public int GridWidth { get; }
        public int GridHeight { get; }
        public GridLayoutSegment[] XSegments { get; }
        public GridLayoutSegment[] YSegments { get; }
        public Boolean DistributeExtra { get; }

        public GridLayout(int width, int height, int gridWidth, int gridHeight, bool distributeExtra = true) : base(width, height)
        {
            GridWidth = gridWidth;
            GridHeight = gridHeight;
            XSegments = new GridLayoutSegment[gridWidth];
            YSegments = new GridLayoutSegment[gridHeight];
            DistributeExtra = distributeExtra;

            for(var x = 0; x < gridWidth; x++)
            {
                XSegments[x] = new GridLayoutSegment();
                XSegments[x].IsDynamic = true;
                XSegments[x].Weight = 1;
            }

            for(var y = 0; y < gridHeight; y++)
            {
                YSegments[y] = new GridLayoutSegment();
                YSegments[y].IsDynamic = true;
                YSegments[y].Weight = 1;
            }

            CalculateDimensions();
        }

        public Rectangle GetGridDimensions(Point gridPosition = default, int gridWidth = 1, int gridHeight = 1)
        {
            var xPosition = 0;
            for(var x = 0; x < gridPosition.X; x++)
            {
                xPosition += XSegments[x].Length;
            }

            var yPosition = 0;
            for(var y = 0; y < gridPosition.Y; y++)
            {
                yPosition += YSegments[y].Length;
            }

            var width = 0;
            for(var x = 0; x < gridWidth; x++)
            {
                width += XSegments[x + gridPosition.X].Length;
            }

            var height = 0;
            for(var y = 0; y < gridHeight; y++)
            {
                height += YSegments[y + gridPosition.Y].Length;
            }

            return new Rectangle(xPosition, yPosition, width, height);
        }

        public T Add<T>(
            Func<int, int, T> ConsoleConstructor, 
            int x = 0, int y = 0, int gridWidth = 1, int gridHeight = 1
            ) where T : SadConsole.Console
        {
            var dimensions = GetGridDimensions(new Point(x, y), gridWidth, gridHeight);

            var console = ConsoleConstructor(dimensions.Width, dimensions.Height);
            console.Position = new Point(dimensions.X, dimensions.Y);
            console.Parent = this;

            return console;
        }

        public void CalculateDimensions()
        {
            CalculateDimension(XSegments, Width);
            CalculateDimension(YSegments, Height);
        }

        public void CalculateDimension(GridLayoutSegment[] segments, int dimensionLength)
        {
            var slack = dimensionLength;
            
            var dynamicSegments = segments.Where(segment => segment.IsDynamic).ToList();
            var fixedSegments = segments.Where(segment => !segment.IsDynamic).ToList();

            foreach(var segment in fixedSegments)
            {
                slack -= segment.Length;
            }

            if(slack < 0)
            {
                throw new System.Exception("Layout space exceeded! You're gonna need a bigger boat!");
            }

            var totalWeight = dynamicSegments.Aggregate(0, (weight, segment) => weight + segment.Weight);
            var slackUsed = 0;

            for(var i = 0; i < dynamicSegments.Count; i++)
            {

                var length = (slack * dynamicSegments[i].Weight) / totalWeight;
                dynamicSegments[i].Length = length;
                slackUsed += length;
            }

            //Spread leftovers
            var index = 0;
            while(DistributeExtra && slack - slackUsed > 0 && dynamicSegments.Count > 0)
            {
                var segment = dynamicSegments[index % dynamicSegments.Count];
                segment.Length += 1;
                index++;
                slackUsed++;
            }
        }
    }
}