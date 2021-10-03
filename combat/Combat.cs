using System.Collections.Generic;
using System.Linq;
using Game.Combat.Event;
using Game.Combat.Action;
using Game.UI.Log;
using Game.Util;

namespace Game.Combat {
    public enum Side
    {
        PLAYER,
        ENEMY,
        NONE
    }

    public class Combat {
        public List<Entity> Combatants { get; }
        public VariableContainer<Entity> Current { get; set; }
        public List<Entity> Initiative { get; set; } = new List<Entity>();
        public Log Log { get; } = new Log();

        public Combat(List<Entity> combatants) 
        {
            Combatants = combatants;
            StartNextTurn();
        }

        public void StartNextTurn()
        {
            Current = GetNextInInitiative();
            new LogAction(Log, $"It's {Current.State.Name}'s turn.").Do();
        }

        public Side GetSide(Entity entity)
        {
            if(Combatants.Contains(entity))
            {
                if(Combatants.IndexOf(entity) < 4)
                {
                    return Side.PLAYER;
                }
                else 
                {
                    return Side.ENEMY;
                }
            }
            else
            {
                return Side.NONE;
            }
        }

        public bool IsOnSameSide(Entity entity1, Entity entity2)
        {
            return GetSide(entity1) == GetSide(entity2);
        }

        public Entity GetNextInInitiative()
        {
            if(Initiative.Count == 0) 
            {
                RollInitiative();
            }

            var entity = Initiative[0];
            Initiative.RemoveAt(0);
            return entity;
        }

        public void RollInitiative()
        {
            var tempInitiative = new List<(Entity, int)>();

            foreach(var entity in Combatants)
            {
                if(entity == null)
                {
                    continue;
                }

                var initiativeValue = GetInitiative(entity);
                var index = 0;
                
                // Sort by proper insertion
                while(index < tempInitiative.Count && initiativeValue <= tempInitiative[index].Item2) 
                {
                    index++;
                }

                tempInitiative.Insert(index, (entity, initiativeValue));
            }

            Initiative = tempInitiative.Select(pair => pair.Item1).ToList();
        }

        public int GetInitiative(Entity entity)
        {
            return 0;
        }

        public void BroadcastCombatEvent(CombatEvent ev)
        {
            ev.Broadcast(Combatants);
        }
    }
}