using Assets._Game.Scripts.Game.Objects.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Factory;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem.Producer
{
    class GemProducer : IGemProducer
    {
        #region VALUES
        private IGemFactory _factory { get; set; }
        #endregion // VALUES

        /////////////////////////////////////

        #region CONSTRUCTOR
        [Inject]
        public GemProducer(IGemFactory factory)
            => _factory = factory;
        #endregion // CONSTRUCTOR

        #region PUBLIC_METHODS
        public IGem Produce()
        {
            var gem = _factory.Create();
            if (gem.Equals(null) == false)
                gem.onBecomeDestroyable += HandleUnused;

            return gem;
        }

        public void HandleUnused(IGem[] gems)
        {
            foreach (var gem in gems)
                HandleUnused(gem);
        }

        public void HandleUnused(IGem gem)
        {
            if (gem.Equals(null) == false) {
                // на случай, если вызов Handle был принудительный
                gem.SetDestroyed();
                UnityEngine.Object.Destroy(gem.gameObject);
            }
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void HandleUnused(IRemoteDestroyable destroyable)
        {
            if (destroyable.Equals(null) == false)
                HandleUnused(destroyable.gameObject.GetComponent<IGem>());
        }
        #endregion // PRIVATE_METHODS
    }
}
