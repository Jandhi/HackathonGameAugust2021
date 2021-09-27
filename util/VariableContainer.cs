namespace Game.Util
{
    public class StateChangeEventArgs<T>
    {
        public T Previous;
        public T Current;
    }

    public class VariableContainer<T>
    {
        private T _state;
        public T State
        {
            get 
            {
                return _state;
            }
            set 
            {
                var prev = _state;

                lock(_state)
                {
                    _state = value;
                }

                RaiseStateChangeEvent(prev, value);
            }
        }
        public static implicit operator T(VariableContainer<T> container) => container.State;
        public static implicit operator VariableContainer<T>(T item) => new VariableContainer<T>
        {
            State = item
        };
        public delegate void StateChangeHandler(object sender, StateChangeEventArgs<T> args);
        public event StateChangeHandler StateChangeEvent;

        protected virtual void RaiseStateChangeEvent(T previous, T current)
        {
            StateChangeEvent?.Invoke(this, new StateChangeEventArgs<T>
            { 
                Previous = previous, 
                Current = current
            });
        }

        public T GetT()
        {
            return State;
        }

        public void Set(T newState)
        {
            State = newState;
        }
    }
}