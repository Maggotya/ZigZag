using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps
{
    class LastStepDetector : ILastStepDetector
    {
        #region PUBLIC_METHODS
        public Vector3[] Detect(Vector3[][] steppedPositions, Vector3 direction, int stepWidth)
        {
            var positions = MultiArrayToOneArray(steppedPositions);

            var indexedPositions =  ConvertToIndexedPositions(positions);
            var scaledElements =    Scale(indexedPositions, direction);
            var orderedElements =   OrderScales(scaledElements, direction);
            var resultIndexes =     GetResultIndexes(orderedElements, stepWidth);

            return resultIndexes.Select(ind => positions[ind]).ToArray();
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private T[] MultiArrayToOneArray<T>(T[][] multiarray)
        {
            var count = multiarray.Sum(array => array.Length);
            var result = new T[count];
            var counter = 0;

            foreach (var array in multiarray) {
                var length = array.Length;
                Array.Copy(array, 0, result, counter, length);
                counter += length;
            }

            return result;
        }

        private IEnumerable<IndexedPosition> ConvertToIndexedPositions(IEnumerable<Vector3> positions)
        {
            var counter = 0;
            return positions.Select(p => new IndexedPosition(counter++, p));
        }

        private IEnumerable<IndexedPosition> Scale(IEnumerable<IndexedPosition> positions, Vector3 direction)
            => positions.Select(p => new IndexedPosition(p.index, Vector3.Scale(p.position, direction)));

        private IEnumerable<IndexedPosition> OrderScales(IEnumerable<IndexedPosition> positions, Vector3 direction)
            => positions.OrderByDescending(GetOrderSelector(direction)).Distinct();

        private Func<IndexedPosition, float> GetOrderSelector(Vector3 direction)
        {
            switch (direction.MaxValueIndex()) {
                case 0: return ip => ip.position.x;
                case 1: return ip => ip.position.y;
                case 2: return ip => ip.position.z;
                default: return ip => ip.position.x;
            }
        }

        private IEnumerable<int> GetResultIndexes(IEnumerable<IndexedPosition> orderedScales, int stepWidth)
            => orderedScales.Take(stepWidth).Select(s => s.index);
        #endregion // PRIVATE_METHODS

        ////////////////////////////////////

        #region PRIVATE_STRUCTS
        private struct IndexedPosition
        {
            public int index;
            public Vector3 position;

            public IndexedPosition(int index, Vector3 position)
            {
                this.index = index;
                this.position = position;
            }
        }
        #endregion // PRIVATE_STRUCTS
    }
}
