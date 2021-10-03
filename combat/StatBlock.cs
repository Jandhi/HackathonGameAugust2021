using System.Collections.Generic;
using System.Linq;

namespace Game.Combat
{

    public class StatChangeEventArgs
    {
        public Entity Entity;
        public Stat Stat;
        public float OldValue;
        public float NewValue;
    }
    
    public class StatBlock
    {
        public Dictionary<Stat, float> Contents { get; } = new Dictionary<Stat, float>();
        public delegate void StatChangeHandler(object sender, StatChangeEventArgs args);
        public event StatChangeHandler StatChangeEvent;
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
                RaiseStatChangeEvent(stat, oldValue, value);
            }
        }

        protected virtual void RaiseStatChangeEvent(Stat stat, float oldValue, float newValue)
        {
            StatChangeEvent?.Invoke(this, new StatChangeEventArgs
            {
                Entity = Parent, 
                Stat = stat,
                OldValue = oldValue, 
                NewValue = newValue
            });
        }

        public StatBlock With(Stat stat, float value)
        {
            this[stat] = value;
            return this;
        }
    }
}