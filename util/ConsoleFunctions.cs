using System;
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
    }
}