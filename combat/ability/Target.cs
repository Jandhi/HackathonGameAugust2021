using System.Collections.Generic;

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

    public static class Target
    {
        public static List<Position> AllPositions => new List<Position>{Position.Front, Position.MidFront, Position.MidBack, Position.Back};
    }
}