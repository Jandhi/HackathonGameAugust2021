using System;
using SadConsole;
using Game.UI;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

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
            var console = new Button("this is a button", () => {
                
            });

            SadConsole.Global.CurrentScreen = console;
        }
    }
}