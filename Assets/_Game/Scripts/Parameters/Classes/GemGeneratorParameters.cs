using System;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [Serializable]
    class GemGeneratorParameters : IGemGeneratorParameters
    {
        #region SERIALIZE_FIELDS
        [Tooltip("Количество платформ в блоке, для которого будет рассматриваться установка одного кристалла")]
        [SerializeField] private int _PlatformsInBlock;

        [Tooltip("Тип алгоритма, по которому будет определяться вероятность установки кристалла в блоке." +
            "Random - на одну случайную платорму из блока. " +
            "Iterative - фиксированный порядковый номер платформы в блоке, который увеличивается на один с каждым блоком.")]
        [SerializeField] private PlacementType _PlacementType;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public int platformsInBlock => _PlatformsInBlock;
        public PlacementType placementType => _PlacementType;
        #endregion // PUBLIC_VALUES
    }

    public enum PlacementType
    { Random, Iterative }
}
