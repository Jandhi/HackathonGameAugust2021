using System.Collections.Generic;
using Game.Combat.Event;

namespace Game.Combat.Ability
{
    public class DamageAbility : Ability
    {
        public int Damage { get; }

        public override void ExecuteAbility(Combat combat, Entity caster, List<Entity> targets)
        {
            
        }
        
    }
}