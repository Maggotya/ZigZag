using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;

namespace Assets._Game.Scripts.Parameters.Classes
{
    [CreateAssetMenu(fileName = "GamePrefabs", menuName = "ScriptableObjects/GamePrefabs")]
    class GamePrefabs : ScriptableObject, IGamePrefabs
    {
        #region SERIALIZE_FIELDS
        [SerializeField] private GameObject _Ball;
        [SerializeField] private GameObject _Gem;
        [SerializeField] private GameObject _Platform;
        #endregion // SERIALIZE_FIELDS

        #region PUBLIC_VALUES
        public GameObject ball => _Ball;
        public GameObject gem => _Gem;
        public GameObject platform => _Platform;
        #endregion // PUBLIC_VALUES
    }
}
