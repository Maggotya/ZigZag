﻿using Assets._Game.Scripts.Game.Objects.Reset;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator
{
    interface IPositionCalculator : IResetable
    {
        int lastStep { get; }
        int[] calculatedSteps { get; }

        bool IsStepCalculated(int step);
        Vector3[] GetPositions(int step);
        void ClearStep(int step);

        void CalculateNextStep(Vector3 prevDirection, Vector3 newDirection, int stepWidth);
    }
}
