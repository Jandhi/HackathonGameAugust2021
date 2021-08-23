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
        public static readonly int GAME_WIDTH = 120;
        public static readonly int GAME_HEIGHT = 40;

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
            
            var entity1 = new Entity("1", null);
            var entity2 = new Entity("2", null);
            var entity3 = new Entity("3", null);
            var entity4 = new Entity("4", null);
            var entity5 = new Entity("5", null);
            var entity6 = new Entity("6", null);
            var entity7 = new Entity("7", null);
            var entity8 = new Entity("8", null);

            var combat = new Combat.Combat(new List<Entity>() {entity1, entity2, entity3, entity4, entity5, entity6, entity7, entity8});

            var ev = new SendDamageEvent(0, combat, entity1, null, null, 5, DamageType.Physical);
            var passive = new Passive<SendDamageEvent>(entity2);
            passive.Filters.Add(ev => ev.Combat.IsOnSameSide(ev.Caster, passive.Parent));
            passive.Modifiers.Add(ev => ev.Damage *= 2f);
            entity2.Passives.Add(passive);

            var passive2 = new Passive<SendDamageEvent>(entity1);
            passive2.Filters.Add(ev => ev.Damage > 7);
            passive2.Modifiers.Add(ev => ev.Damage *= 2f);
            entity1.Passives.Add(passive2);

            ev.Broadcast();

            var display = new CombatDisplay(GAME_WIDTH, GAME_HEIGHT, combat);
            display.Parent = console;
            
            SadConsole.Global.CurrentScreen = console;
        }
    }
}