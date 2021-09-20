using Game.Combat.Action;
using System.Collections.Generic;

namespace Game.Combat.Ability
{
    public abstract class AbilityComponent
    {
        public abstract void Execute(Ability ability, int depth, Combat combat, AbilityResult root, Entity caster, List<Entity> targets);
    }
}