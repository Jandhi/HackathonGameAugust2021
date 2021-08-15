using System.Collections.Generic;

namespace Game.Combat.Ability
{
    public interface IAbility
    {
        string Name { get; }

        void UseAbility(Combat combat, Entity caster, List<Entity> targets);
        
    }
}