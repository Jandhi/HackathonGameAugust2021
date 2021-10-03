using System;

namespace Game.Util
{
    public class LinkedContainer<TItem, TSource> : VariableContainer<TItem>
    {
        public LinkedContainer(VariableContainer<TSource> source, Func<TSource, TItem> converter)
        {
            source.StateChangeEvent += (obj, args) => {
                Set(converter(args.NewValue));
            };

            Set(converter(source.State));
        }
    }
}