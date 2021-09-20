using System.Collections.Generic;
using Game.Combat.Event;
using Game.UI;
using Game.Combat.Action;

namespace Game.Combat.Ability
{
    public enum TargetType
    {
        Self, Allies, Opponents, Any
    }
    
    public enum Position
    {
        Front, MidFront, MidBack, Back
    }

    public abstract class Ability : INamed
    {
        public ColoredString Name { get; }
        public TargetType Target { get; }
        public List<Position> TargetPositions { get; }
        public List<Position> CasterPositions { get; }
        public Ability(ColoredString name, TargetType target, List<Position> targetPositions, List<Position> casterPositions)
        {
            Name = name;
            Target = target;
            TargetPositions = targetPositions;
            CasterPositions = casterPositions;
        }
        public AbilityResult Use(int depth, Combat combat, Entity caster, List<Entity> targets)
        {
            var root = new AbilityResult(this, caster);

            var useAbilityEvent = new UseAbilityEvent(depth, combat, root, caster, this);
            useAbilityEvent.Broadcast();
            
            if(useAbilityEvent.IsGoingThrough) {
                Execute(depth, combat, root, caster, targets);
            }

            return root;
        }

        public abstract void Execute(int depth, Combat combat, AbilityResult root, Entity caster, List<Entity> targets);

        public bool CanCastFromPosition(Entity caster, Combat combat)
        {
            var isInPosition = CasterPositions.Contains(IndexToPosition(combat.Combatants.IndexOf(caster)));
            var hasTargets = GetValidTargets(caster, combat).Count > 0;

            return isInPosition && hasTargets;
        }
        public List<int> GetValidTargets(Entity caster, Combat combat)
        {
            var side = combat.GetSide(caster);
            var validTargets = new List<int>();

            if(Target == TargetType.Self)
            {
                validTargets.Add(combat.Combatants.IndexOf(caster));
            }

            foreach(var position in TargetPositions)
            {
                if(Target == TargetType.Any || (side == Side.PLAYER && Target == TargetType.Allies) || (side == Side.ENEMY && Target == TargetType.Opponents) )
                {
                    validTargets.Add( PositionToIndex(position, Side.PLAYER) );
                }

                if(Target == TargetType.Any || (side == Side.PLAYER && Target == TargetType.Opponents) || (side == Side.ENEMY && Target == TargetType.Allies) )
                {
                    validTargets.Add( PositionToIndex(position, Side.ENEMY) );
                }
            }

            return validTargets;
        }

        private int PositionToIndex(Position position, Side side) => position switch
        {
            Position.Front => side == Side.PLAYER ? 3 : 4,
            Position.MidFront => side == Side.PLAYER ? 2 : 5,
            Position.MidBack => side == Side.PLAYER ? 1 : 6,
            Position.Back => side == Side.PLAYER ? 0 : 7,
            _ => -1,
        };
        
        private Position IndexToPosition(int index)
        {
            if(index == 0 || index == 7) return Position.Back;
            if(index == 1 || index == 6) return Position.MidBack;
            if(index == 2 || index == 5) return Position.MidFront;
            if(index == 3 || index == 4) return Position.Front;
            
            return Position.Front;
        }
    }
}