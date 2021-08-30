using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game.Util
{
    public static class ConsoleFunctions
    {
        // Recursively applies a function to a console and its children
        public static void DoRecursive(SadConsole.Console console, Action<SadConsole.Console> action)
        {
            var actionQueue = new Queue<SadConsole.Console>();
            actionQueue.Enqueue(console);

            while(actionQueue.Count > 0)
            {
                var nextConsole = actionQueue.Dequeue();

                foreach(var child in nextConsole.Children)
                {
                    actionQueue.Enqueue(child);
                }

                action(nextConsole);
            }
        }

        public static bool IsInConsole(Point point, SadConsole.Console console)
        {
            var consoleWorldPosition = GetWorldPosition(console);
            return point.X >= consoleWorldPosition.X &&
                point.X < consoleWorldPosition.X + console.Width &&
                point.Y >= consoleWorldPosition.Y &&
                point.Y < consoleWorldPosition.Y + console.Height;
        }

        public static Point GetWorldPosition(SadConsole.Console console)
        {
            var point = new Point();
            var node = console;

            while(node != null)
            {
                point += node.Position;
                node = node.Parent;
            }

            return point;
        }
    }
}