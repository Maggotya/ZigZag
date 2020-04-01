
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Factory;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Objects.Platform;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Producer
{
    class PlatformProducer : IPlatformProducer
    {
        #region VALUES
        private IPlatformFactory _factory { get; set; }
        #endregion // VALUES

        /////////////////////////////////////

        #region CONSTRUCTOR
        [Inject]
        public PlatformProducer(IPlatformFactory factory)
            => _factory = factory;
        #endregion // CONSTRUCTOR

        #region PUBLIC_METHODS
        public IPlatform Produce()
        {
            var platform = _factory.Create();
            if (platform.Equals(null) == false)
                platform.onBecomeDestroyable += HandleUnused;

            return platform;
        }

        public void HandleUnused(IPlatform[] platforms)
        {
            foreach (var platfrom in platforms)
                HandleUnused(platfrom);
        }

        public void HandleUnused(IPlatform platform)
        {
            if (platform.Equals(null) == false) {
                // на случай, если вызов Handle был принудительный
                platform.SetDestroyed();
                UnityEngine.Object.Destroy(platform.gameObject);
            }
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void HandleUnused(IRemoteDestroyable destroyable)
        {
            if (destroyable.Equals(null) == false)
                HandleUnused(destroyable.gameObject.GetComponent<IPlatform>());
        }
        #endregion // PRIVATE_METHODS
    }
}
