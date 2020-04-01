using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps
{
    interface ILastStepDetector
    {
        Vector3[] Detect(Vector3[][] steppedPositions, Vector3 direction, int stepWidth);
    }
}
