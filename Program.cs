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

            var text = "";
            for(var i = 0; i < 100; i++) text += (char) (new Random().Next() % 24 + 'a');
            
            var display = new ScrollableTextDisplay(5, 10, text);
            display.Parent = console;
            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}