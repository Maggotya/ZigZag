using System;
using UnityEngine;

namespace Assets._Game.Scripts.Instruments.Counter
{
    class IntCounter : ICounter<int>
    {
        #region PUBLIC_VALUES
        public bool looped { get; private set; }
        public int step { get; private set; }
        public int start { get; private set; }
        public int end { get; private set; }
        public int current { get; private set; }

        public int next => CorrectValue(current + step);
        public int prev => CorrectValue(current - step);

        public Action<int> onChanged { get; set; }
        #endregion // PUBLIC_VALUES

        #region PRIVATE_VALUES
        private int _intervalLength =>
            Mathf.Abs(end - start) + 1;
        #endregion // PRIVATE_VALUES

        ///////////////////////////////////////////////////

        #region CONSTRUCTORS
        public IntCounter()
        {
            looped = false;
            step = 1;

            start = 0;
            end = int.MaxValue;

            current = start;
        }

        public IntCounter(int step) : this()
            => this.step = step;

        public IntCounter(int current, int step) : this(step)
            => SetCurrent(current);

        public IntCounter(int current, int step, bool looped) : this(current, step)
            => this.looped = looped;

        public IntCounter(int current, int step, int end, bool looped) : this(current, step, looped)
        {
            this.end = end;
            CorrectBounds();
        }

        public IntCounter(int current, int step, int start, int end, bool looped) : this(current, step, looped)
        {
            this.start = start;
            this.end = end;
            CorrectBounds();
        }
        #endregion // CONSTRUCTORS

        #region PUBLIC_METHODS
        public int Increase()
            => SetCurrent(next);

        public int Dicrease()
            => SetCurrent(prev);

        public void Reset()
            => SetCurrent(start);

        public int SetCurrent(int value)
        {
            current = looped ?
                CorrectValueInLoop(value) :
                CorrectValueBounded(value);

            onChanged?.Invoke(current);
            return current;
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private int CorrectValue(int value)
            => looped ? CorrectValueInLoop(value) : CorrectValueBounded(value);

        private int CorrectValueInLoop(int value)
        {
            while (value > end)
                value -= _intervalLength;
            while (value < start)
                value += _intervalLength;

            return value;
        }

        private int CorrectValueBounded(int value)
            => Mathf.Clamp(value, start, end);


        private void CorrectBounds()
        {
            if (NeedSwapBounds()) {
                var buf = start;
                start = end;
                end = buf;
            }
        }

        private bool NeedSwapBounds()
            => ( ( end < start ) && ( step >= 0 ) ) ||
                ( ( start < end ) && ( step < 0 ) );
        #endregion // PRIVATE_METHODS
    }

}
