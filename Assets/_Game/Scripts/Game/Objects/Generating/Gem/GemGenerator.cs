using System.Collections.Generic;
using Assets._Game.Scripts.Game.Handlers.Probability;
using Assets._Game.Scripts.Game.Objects.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Producer;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Variator;
using Assets._Game.Scripts.Game.Objects.Platform;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem
{
    class GemGenerator : MonoBehaviour, IGemGenerator
    {
        #region SERIALIZE_FIELDS
        [Header("Attachments")]
        [SerializeField] private Transform _Container;

        [Header("Config")]
        [SerializeField] private bool _Active;

        [Header("Events")]
        [SerializeField] private UnityEvent _OnCollected;
        #endregion // SERIALIZE_FIELDS

        #region PRIVATE_VALUES
        private IVariator _variator { get; set; }
        private IGemProducer _producer { get; set; }
        private SafeHashSet<IGem> _gems { get; set; }
        #endregion // PRIVATE_VALUES

        #region PUBLIC_VALUES
        public bool active => _Active;
        public UnityEvent onCollected {
            get => _OnCollected;
            set => _OnCollected = value;
        }
        #endregion // PUBLIC_VALUES

        ////////////////////////////////////////////

        #region INJECTS
        [Inject]
        public void Construct(IGemProducer gemProducer, IVariator variator)
        {
            _producer = gemProducer;
            _variator = variator;
            _gems = new SafeHashSet<IGem>();
        }
        #endregion // INJECTS

        #region PUBLIC_METHODS
        public void SetActive(bool status)
        {
            if (_Active == status)
                return;
            _Active = status;
        }

        public void TryGenerate(IPlatform platform)
        {
            if (active == false || platform.Equals(null))
                return;
            if (_variator.IsSuccess() == false)
                return;

            var gem = ConfigureGem(_producer.Produce(), platform);
            _gems.Add(gem);
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private IGem ConfigureGem(IGem gem, IPlatform platform)
        {
            if (gem.Equals(null))
                return null;

            gem.gameObject.transform.parent = _Container;
            gem.SetPositionOnSurface(platform.positionToPlace);

            gem.onCollected += OnCollected;

            platform.onBecomeDestroyable += (p) => gem.SetDestroyed();
            platform.onDeactivating += gem.Deactivate;

            return gem;
        }

        private void OnCollected(IGem booster)
        {
            if (active == false)
                return;
            if (booster.Equals(null))
                return;

            _gems.Remove(booster);
            onCollected?.Invoke();
        }
        #endregion // PRIVATE_METHODS
    }
}
