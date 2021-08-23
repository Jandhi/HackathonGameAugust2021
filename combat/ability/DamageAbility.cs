using System.Collections.Generic;
using Game.Combat.Event;
using Game.Combat.Action;

namespace Game.Combat.Ability
{
    public class DamageAbility : Ability
    {
        public float Damage { get; }
        public DamageType Type { get; }

        public override void Execute(int depth, Combat combat, Entity caster, List<Entity> targets)
        {
            var sendDamageEvent = new SendDamageEvent(depth, combat, caster, targets, this, Damage, Type);
            sendDamageEvent.Broadcast();

            if(!sendDamageEvent.IsGoingThrough)
            {
                return;
            }

            foreach(var target in sendDamageEvent.Targets)
            {
                var receiveDamageEvent = new ReceiveDamageEvent(sendDamageEvent, target);
                receiveDamageEvent.Broadcast();

                if(!receiveDamageEvent.IsGoingThrough)
                {
                    return;
                }

                var before = target.Stats[Stat.Health];
                var after = before - Damage;

                combat.Actions.Enqueue(new StatChange(target, Stat.Health, before, after));
            }
        }
        
    }
}