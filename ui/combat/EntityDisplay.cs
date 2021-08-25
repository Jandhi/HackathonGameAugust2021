using Microsoft.Xna.Framework;
using Game.Combat;

namespace Game.UI.Combat
{
    public class EntityDisplay : GridLayout, IDrawable
    {
        public static readonly int EntityDisplayGridWidth = 1;
        public static readonly int EntityDisplayGridHeight = 5;

        public Entity Entity { get; set; } = null;
        public EntityDisplay(int width, int height) : base(width, height, EntityDisplayGridWidth, EntityDisplayGridHeight)
        {
            SetupLayout();
        }
        
        public void UpdateEntity(Entity entity)
        {
            Entity = entity;
            Clear();
            Draw();
        }

        public void Draw()
        {
            
        }

        public void SetupLayout()
        {
            YSegments[0].Length = 1;
            YSegments[0].IsDynamic = false;


            CalculateDimensions();
        }
    }
}