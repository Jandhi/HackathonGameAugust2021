using System;
using System.Collections.Generic;
using Game.Combat.Action;

namespace Game.Combat.Ability
{
    public class ConditionalComponent : AbilityComponent
    {
        public Func<int, int, Combat, ActionRoot, Entity, List<Entity>, bool> Condition { get; }
        public AbilityComponent Child { get; }

        public ConditionalComponent(Func<int, int, Combat, ActionRoot, Entity, List<Entity>, bool> condition, AbilityComponent child)
        {
            Condition = condition;
            Child = child;
        }

        public override void Execute(int seed, int depth, Combat combat, ActionRoot root, Entity caster, List<Entity> targets)
        {
            if(Condition(seed, depth, combat, root, caster, targets))
            {
                Child.Execute(seed, depth, combat, root, caster, targets);
            }
        }
    }
}