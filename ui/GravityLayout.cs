using System;
using Microsoft.Xna.Framework;

namespace Game.UI
{
    
    public enum LayoutGravity
    {
        TOP, CENTER, BOTTOM
    }

    public class GravityLayout : SadConsole.Console
    {
        public SadConsole.Console Containee { get; set; }

        public GravityLayout(int width, int height) : base(width, height)
        {}

        public T Add<T>(Func<int, int, T> consoleConstructor, 
            int fixedWidth = -1, bool xBufferIsFixed = false, LayoutGravity xGravity = LayoutGravity.TOP, 
            int fixedHeight = -1, bool yBufferIsFixed = false, LayoutGravity yGravity = LayoutGravity.TOP) where T : SadConsole.Console
        {
            if(Width < fixedWidth || Height < fixedHeight) 
            {
                throw new ArgumentException("Fixed dimensions cannot be larger than Gravit Layout!");
            }

            var xPos = 0;
            var yPos = 0;
            var consoleWidth = Width;
            var consoleHeight = Height;

            if(fixedWidth != -1)
            {
                consoleWidth = fixedWidth;
                var buffer = Width - fixedWidth;

                if(xBufferIsFixed)
                {
                    consoleWidth = Width - fixedWidth;
                    buffer = fixedWidth;
                }

                if(xGravity == LayoutGravity.CENTER)
                {
                    xPos = buffer / 2;
                }
                else if(xGravity == LayoutGravity.BOTTOM)
                {
                    xPos = buffer;
                }
            }

            if(fixedHeight != -1)
            {
                consoleHeight = fixedHeight;
                var buffer = Height - fixedHeight;

                if(yBufferIsFixed)
                {
                    consoleHeight = Height - fixedHeight;
                    buffer = fixedHeight;
                }

                if(yGravity == LayoutGravity.CENTER)
                {
                    yPos = buffer / 2;
                }
                else if(yGravity == LayoutGravity.BOTTOM)
                {
                    yPos = buffer;
                }
            }

            var containee = consoleConstructor(consoleWidth, consoleHeight);
            containee.Position = new Point(xPos, yPos);
            containee.Parent = this;
            Containee = containee;
            return containee;
        }
    }
}