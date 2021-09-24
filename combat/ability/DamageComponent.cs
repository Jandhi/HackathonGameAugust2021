using Game.Combat;
using Game.Combat.Action;
using Game.Combat.Event;
using System.Collections.Generic;

namespace Game.Combat.Ability
{
    public class StaticDamageComponent : DamageComponent
    {
        public float Damage { get; }
        public DamageType Type { get; }

        public StaticDamageComponent(float damage, DamageType type)
        {
            Damage = damage;
            Type = type;
        }

        public override float GetDamage(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets)
        {
            return Damage;
        }

        public override DamageType GetDamageType(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets)
        {
            return Type;
        }
    }

    public abstract class DamageComponent : AbilityComponent
    {
        public abstract float GetDamage(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets);
        public abstract DamageType GetDamageType(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets);
        public override void Execute(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets)
        {
            var damage = GetDamage(seed, depth, combat, root, caster, targets);
            var type = GetDamageType(seed, depth, combat, root, caster, targets);
            var sendDamageEvent = new SendDamageEvent(depth, combat, root, caster, targets, damage, type);
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