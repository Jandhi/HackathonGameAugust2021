using System.Collections.Generic;
using SadConsole;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Console = SadConsole.Console;
using Game.UI.Combat;
using Game.Combat;
using Game.Combat.Event;
using Game.Audio;
using Game.Combat.Action;

namespace Game
{
    class Program
    {
        public static readonly int GAME_WIDTH = 150;
        public static readonly int GAME_HEIGHT = 50;

        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(GAME_WIDTH, GAME_HEIGHT);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            AudioManager.Init();

            var console = new Console(GAME_WIDTH, GAME_HEIGHT);
            var entity4 = new Entity(new Game.UI.ColoredString("Ratman", Color.Gray), null);
            var entity5 = new Entity(new Game.UI.ColoredString("Lizardman", Color.Green), null);

            var combat = new Combat.Combat(new List<Entity>() {null, null, null, entity4, entity5, null, null, null});

            var display = new CombatDisplay(GAME_WIDTH, GAME_HEIGHT, combat);
            display.Parent = console;


            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}