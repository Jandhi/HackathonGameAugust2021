using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat.Ability
{
    public abstract class Ability
    {
        public string Name { get; }

        public void UseAbility(Combat combat, Entity caster, List<Entity> targets)
        {
            combat.BroadcastCombatEvent(new UseAbilityEvent(0, combat, caster, this));
            ExecuteAbility(combat, caster, targets);
        }

        public abstract void ExecuteAbility(Combat combat, Entity caster, List<Entity> targets);
        
    }
}