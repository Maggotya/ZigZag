using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps
{
    class StepCalculator : IStepCalculator
    {
        #region PRIVATE_VALUES
        private float _platformSize { get; set; }
        #endregion // PRIVATE_VALUES

        //////////////////////////////////////////////////

        #region CONSTRUCTOR
        [Inject]
        public StepCalculator(float platfromSize)
        {
            _platformSize = platfromSize;
        }
        #endregion // CONSTRUCTOR

        #region PUBLIC_METHODS
        public Vector3[][] Rect(Vector3[] prevPositions, Vector3 direction, int stepWidth)
        {
            var result = new Vector3[stepWidth][];

            for (var i = 0; i < stepWidth; i++) {
                var prevResults = i == 0 ? prevPositions : result[i - 1];
                result[i] = Step(prevResults, direction, stepWidth);
            }

            return result;
        }

        public Vector3[] Step(Vector3[] prevPositions, Vector3 direction, int stepWidth)
        {
            direction.Normalize();
            var normalDirection = direction.NormalOnHorizontalPlane();

            return TranslateRowMidTo(
                CreateRow(normalDirection, stepWidth),
                GetNextPoint(GetMidPoint(prevPositions), direction));
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private Vector3[] TranslateRowMidTo(Vector3[] row, Vector3 targetPoint)
        {
            var mid = GetMidPoint(row);
            var direction = targetPoint - mid;

            for(var i = 0; i < row.Length; i++)
                row[i] += direction;

            return row;
        }

        private Vector3 GetNextPoint(Vector3 startPoint, Vector3 normilizedDirecion)
            => startPoint + normilizedDirecion * _platformSize;

        private Vector3 GetMidPoint(Vector3[] positions)
            => positions.Length == 0 ? Vector3.zero :
            positions.Aggregate((prev, next) => prev + next) / positions.Length;

        private Vector3[] CreateRow(Vector3 direction, int length)
        {
            var result = new Vector3[length];
            var offset = direction * _platformSize;

            for (var i = 0; i < length; i++)
                result[i] = offset * i;

            return result;
        }
        #endregion // PRIVATE_METHODS
    }
}
