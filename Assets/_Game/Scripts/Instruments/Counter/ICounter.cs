using System;

namespace Assets._Game.Scripts.Instruments.Counter
{
    interface ICounter<T>
    {
        bool looped { get; }
        T step { get; }
        T start { get; }
        T end { get; }
        T current { get; }
        
        T next { get; }
        T prev { get; }
        Action<T> onChanged { get; set; }

        T Increase();
        T Dicrease();
        T Reset();
        T SetCurrent(T value);
    }
}
