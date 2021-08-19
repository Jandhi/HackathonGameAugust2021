using SadConsole;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    public class LayoutSegment
    {
        public bool IsDynamic { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
    }

    public class Layout : SadConsole.Console
    {
        public int GridWidth { get; }
        public int GridHeight { get; }
        public LayoutSegment[] XSegments { get; }
        public LayoutSegment[] YSegments { get; }

        public Layout(int width, int height, int gridWidth, int gridHeight) : base(width, height)
        {
            GridWidth = gridWidth;
            GridHeight = gridHeight;
            XSegments = new LayoutSegment[gridWidth];
            YSegments = new LayoutSegment[gridHeight];

            for(var x = 0; x < gridWidth; x++)
            {
                XSegments[x] = new LayoutSegment();
                XSegments[x].IsDynamic = true;
                XSegments[x].Weight = 1;
            }

            for(var y = 0; y < gridHeight; y++)
            {
                YSegments[y] = new LayoutSegment();
                YSegments[y].IsDynamic = true;
                YSegments[y].Weight = 1;
            }

            CalculateDimensions();
        }

        public void Add(Func<int, int, SadConsole.Console> ConsoleConstructor, Point gridPosition = default, int gridWidth = 1, int gridHeight = 1)
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

            System.Console.Write($"{xPosition} {yPosition} {width} {height}");

            var console = ConsoleConstructor(width, height);
            console.Position = new Point(xPosition, yPosition);
            console.Parent = this;
        }

        public void CalculateDimensions()
        {
            CalculateDimension(XSegments, Width);
            CalculateDimension(YSegments, Height);
        }

        public void CalculateDimension(LayoutSegment[] segments, int dimensionLength)
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
            while(slack - slackUsed > 0 && dynamicSegments.Count > 0)
            {
                var segment = dynamicSegments[index % dynamicSegments.Count];
                segment.Length += 1;
                index++;
                slackUsed++;
            }
        }
    }
}