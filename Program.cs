using System.Collections.Generic;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using Game.UI.Combat;
using Game.Combat;
using Game.Combat.Event;

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
            var console = new Console(GAME_WIDTH, GAME_HEIGHT);
            
            var entity1 = new Entity(new Game.UI.ColoredString("1", Color.Red), null, new List<Tag>{
                new Tag(new UI.ColoredString("tag", Color.BlueViolet)),
                new Tag(new UI.ColoredString("tag2", Color.SpringGreen))
            });
            var entity2 = new Entity(new Game.UI.ColoredString("2", Color.Red), null);
            var entity3 = new Entity(new Game.UI.ColoredString("3", Color.Red), null);
            var entity4 = new Entity(new Game.UI.ColoredString("4", Color.Green), null);
            var entity5 = new Entity(new Game.UI.ColoredString("5", Color.Azure), null);
            var entity6 = new Entity(new Game.UI.ColoredString("6", Color.Beige), null);
            var entity7 = new Entity(new Game.UI.ColoredString("7", Color.Yellow), null);
            var entity8 = new Entity(new Game.UI.ColoredString("8", Color.YellowGreen), null);

            var combat = new Combat.Combat(new List<Entity>() {entity1, entity2, entity3, entity4, entity5, entity6, entity7, entity8});

            var root = new Combat.Action.AbilityResult(entity1);
            var ev = new SendDamageEvent(0, combat, root, entity1, null, null, 5, DamageType.Physical);
            var passive = new Passive<SendDamageEvent>(
                name: new UI.ColoredString("test"), 
                parent: entity2
            );
            passive.Filters.Add(ev => ev.Combat.IsOnSameSide(ev.Caster, passive.Parent));
            passive.Modifiers.Add(ev => ev.Damage *= 2f);

            var passive2 = new Passive<SendDamageEvent>(new UI.ColoredString("test"), entity1);
            passive2.Filters.Add(ev => ev.Damage > 7);
            passive2.Modifiers.Add(ev => ev.Damage *= 2f);

            ev.Broadcast();

            var display = new CombatDisplay(GAME_WIDTH, GAME_HEIGHT, combat);
            display.Parent = console;


            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}