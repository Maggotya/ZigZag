
namespace System.Collections.Generic
{
    class SafeHashSet<T> : HashSet<T>
    {
        public new void Add(T value)
        {
            if (Contains(value))
                return;

            base.Add(value);
        }

        public new void Remove(T value)
        {
            if (Contains(value) == false)
                return;

            base.Remove(value);
        }
    }
}
