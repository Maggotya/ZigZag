using System.Collections.Generic;
using System.Linq;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator
{
    class PositionCalculator : IPositionCalculator
    {
        #region PRIVATE_VALUES
        private Dictionary<int, Vector3[]> _preCalculatedPositions { get; set; }
        private ILastStepDetector _lastStepDetector { get; set; }
        private IStepCalculator _stepCalculator { get; set; }
        private Vector3[] _positionsOnLastStep { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public int lastStep { get; private set; }
        public int[] calculatedSteps => _preCalculatedPositions.Keys.ToArray();
        #endregion // PUBLIC_VALUES

        #region CONSTS
        private Vector3[] DEFAULT_START_POSITIONS => new Vector3[] { Vector3.zero };
        private const int DEFAULT_LAST_STEP = 0;
        #endregion // CONSTS

        ///////////////////////////////////////////////

        #region CONSTRUCTOR
        [Inject]
        public PositionCalculator(IStepCalculator stepCalculator, ILastStepDetector lastStepDetector)
        {
            _stepCalculator = stepCalculator;
            _lastStepDetector = lastStepDetector;

            lastStep = DEFAULT_LAST_STEP;
            _positionsOnLastStep = DEFAULT_START_POSITIONS;
            _preCalculatedPositions = new Dictionary<int, Vector3[]>();
        }
        #endregion // CONSTRUCTOR

        ///////////////////////////////////////////////

        #region STEP_OEPRATIONS
        public bool IsStepCalculated(int step)
            => _preCalculatedPositions.ContainsKey(step);

        public Vector3[] GetPositions(int step)
            => IsStepCalculated(step) ?
            _preCalculatedPositions[step] :
            new Vector3[0];

        public void ClearStep(int step)
        {
            if (IsStepCalculated(step))
                _preCalculatedPositions.Remove(step);
        }
        #endregion // STEP_OEPRATIONS

        #region RESET
        public void Reset()
        {
            _preCalculatedPositions.Clear();

            lastStep = DEFAULT_LAST_STEP;
            _positionsOnLastStep = DEFAULT_START_POSITIONS;
        }
        #endregion // RESET

        #region PRECALCULATION_HANDLING
        public void CalculateNextStep(Vector3 prevDirection, Vector3 newDirection, int stepWidth)
        {
            if (newDirection.Equals(prevDirection))
                CalculateSameDirection(prevDirection, stepWidth);
            else
                PrecalculateNewDirection(prevDirection, newDirection, stepWidth);
        }

        private void CalculateSameDirection(Vector3 direction, int stepWidth)
        {
             // Если направление не меняется, то создаём один шаг заданной ширины
            var positions = _stepCalculator.Step(_positionsOnLastStep, direction, stepWidth);
            _preCalculatedPositions[lastStep++] = positions;
            _positionsOnLastStep = positions;
        }

        private void PrecalculateNewDirection(Vector3 prevDirection, Vector3 newDirection, int stepWidth)
        {
            /*
             * Если направление поменялось, то создаём квадрат платформ размера ширины шага.
             * Это обеспечивает тот факт, что при повороте полоса заданой ширины точно будет иметь в основании
             * достаточное количество платформ с прдыдущего направления.
             * Это важно, т.к. направление может поменяться на каждом шаге, т.е. на текущем направлении 
             * может оказаться обработано недостаточно шагов для начала нового. 
             * Можно было, конечно, просто на каждом шаге проверять, можно ли менять направление сейчас
             * при заданой ширине шага. Но я подумал, что так получится безопаснее и прозрачнее в перспективе.
             */
            var rect = _stepCalculator.Rect(_positionsOnLastStep, prevDirection, stepWidth);

            foreach (var positions in rect)
                _preCalculatedPositions[lastStep++] = positions;

            _positionsOnLastStep = _lastStepDetector.Detect(rect, newDirection, stepWidth);
        }
        #endregion // PRECALCULATION_HANDLING
    }
}
