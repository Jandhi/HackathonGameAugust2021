using System.Collections;
using System.Collections.Generic;

namespace Game.Util
{
    public class EnumerableContainer<TItem, TEnumerable> : VariableContainer<TEnumerable>, IEnumerable<TItem> where TEnumerable : IEnumerable<TItem>
    {
        public EnumerableContainer(TEnumerable startValue = default) : base(startValue)
        {

        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return State.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
