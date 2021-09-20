using Game.Combat;
using Game.Combat.Action;
using Game.Combat.Event;
using System.Collections.Generic;

namespace Game.Combat.Ability
{
    public class DamageComponent
    {
        public float Damage { get; }
        public DamageType Type { get; }

        public void Execute(Ability ability, int depth, Combat combat, AbilityResult root, Entity caster, List<Entity> targets)
        {
            var sendDamageEvent = new SendDamageEvent(depth, combat, root, caster, targets, ability, Damage, Type);
            combat.BroadcastCombatEvent(sendDamageEvent);

            if(!sendDamageEvent.IsGoingThrough)
            {
                return;
            }

            foreach(var target in targets)
            {
                var tookDamageEvent = target.ReceiveDamage(sendDamageEvent);
                tookDamageEvent?.Broadcast();
            }
        }
    }
}