using System.Collections.Generic;
using System.Linq;

namespace Game.Combat
{
    public interface IStatChangeListener
    {
        void NotifyStatChange(Entity entity, Stat stat, float oldValue, float newValue);
    }
    
    public class StatBlock
    {
        public Dictionary<Stat, float> Contents { get; } = new Dictionary<Stat, float>();
        public List<IStatChangeListener> Listeners { get; } = new List<IStatChangeListener>();
        public Entity Parent { get; set; }

        public float this[Stat stat]
        {
            get 
            {  
                if(Contents.ContainsKey(stat))
                {
                    return Contents[stat];
                }
                else
                {
                    return 0;
                }
            }
            set 
            {
                var oldValue = this[stat];
                Contents[stat] = value;
                Listeners.ForEach(listener => listener.NotifyStatChange(Parent, stat, oldValue, value));
            }
        }
    }
}