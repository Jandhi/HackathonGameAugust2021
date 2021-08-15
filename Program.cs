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
            
            var healthBar = new HealthBar(4, 10, 10);
            healthBar.Parent = console;
            healthBar.Position = new Point(10, 10);
            
            var button1 = new Button("down", () => {
                healthBar.Health -= 1;
            });
            button1.Parent = console;
            button1.Position = new Point(2, 10);

            var button2 = new Button("up", () => {
                healthBar.Health += 1;
            });
            button2.Parent = console;
            button2.Position = new Point(25, 10);
            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}