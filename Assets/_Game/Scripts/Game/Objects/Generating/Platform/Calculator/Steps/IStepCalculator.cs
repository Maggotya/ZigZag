using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps
{
    interface IStepCalculator
    {
        Vector3[][] Rect(Vector3[] prevPositions, Vector3 direction, int stepWidth);
        Vector3[] Step(Vector3[] prevPositions, Vector3 direction, int stepWidth);
    }
}
