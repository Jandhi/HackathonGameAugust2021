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
            
            var layout = new Layout(80, 25, 3, 3);
            layout.Parent = console;
            layout.YSegments[2].IsDynamic = false;
            layout.YSegments[2].Length = 5;
            layout.XSegments[0].Weight = 2;
            layout.CalculateDimensions();

            layout.Add((width, height) => {
                return new Console(width, height);
            }).Fill(Color.Red, Color.Red, 0);

            layout.Add((width, height) => {
                return new Console(width, height);
            }, new Point(1, 0), 1, 2).Fill(Color.Blue, Color.Blue, 0);

            layout.Add((width, height) => {
                return new Console(width, height);
            }, new Point(2, 0), 1, 2).Fill(Color.Yellow, Color.Yellow, 0);

            layout.Add((width, height) => {
                return new Console(width, height);
            }, new Point(0, 2), 3).Fill(Color.Green, Color.Green, 0);
            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}