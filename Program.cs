using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using Game.UI;

namespace Game
{
    class Program
    {
        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(80, 25);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            var console = new Console(80, 25);
            
            var layout = new Layout(80, 25, 2, 2);
            layout.Parent = console;
            layout.YSegments[1].IsDynamic = false;
            layout.YSegments[1].Length = 5;
            layout.XSegments[0].Weight = 2;
            layout.CalculateDimensions();

            Console console1 = null;
            layout.Add((width, height) => {
                console1 = new Console(width, height);
                return console1;
            });
            console1.FillWithRandomGarbage();

            Console console2 = null;
            layout.Add((width, height) => {
                console2 = new Console(width, height);
                return console2;
            }, new Point(1, 0));
            console2.Print(0, 0, "This is a console");

            Console console3 = null;
            layout.Add((width, height) => {
                console3 = new Console(width, height);
                return console3;
            }, new Point(0, 1), 2);
            console3.FillWithRandomGarbage();
            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}