using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat.Ability
{
    public abstract class Ability
    {
        public string Name { get; }

        public void Use(int depth, Combat combat, Entity caster, List<Entity> targets)
        {
            var useAbilityEvent = new UseAbilityEvent(depth, combat, caster, this);
            useAbilityEvent.Broadcast();
            
            if(useAbilityEvent.IsGoingThrough) {
                Execute(depth, combat, caster, targets);
            }
        }

        public abstract void Execute(int depth, Combat combat, Entity caster, List<Entity> targets);
        
    }
}