using Assets._Game.Scripts.Game.Objects.Interfaces;

namespace Assets._Game.Scripts.Game.Objects.Generating
{
    abstract class AbstractProducer<T> : IProducer<T> where T : IRemoteDestroyable
    {
        #region VALUES
        private IFactory<T> _factory { get; set; }
        #endregion // VALUES

        /////////////////////////////////////

        #region CONSTRUCTOR
        public AbstractProducer(IFactory<T> factory)
            => _factory = factory;
        #endregion // CONSTRUCTOR

        #region PUBLIC_METHODS
        public T Produce()
        {
            var entity = _factory.Create();
            if (entity.Equals(null) == false)
                entity.onBecomeDestroyable += HandleUnused;

            return entity;
        }

        public void HandleUnused(T[] entities)
        {
            foreach (var entity in entities)
                HandleUnused(entity);
        }

        public void HandleUnused(T entity)
        {
            if (entity.Equals(null) == false) {
                // на случай, если вызов Handle был принудительный
                entity.SetDestroyed();
                UnityEngine.Object.Destroy(entity.gameObject);
            }
        }
        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS
        private void HandleUnused(IRemoteDestroyable destroyable)
        {
            if (destroyable.Equals(null) == false)
                HandleUnused(destroyable.gameObject.GetComponent<T>());
        }
        #endregion // PRIVATE_METHODS
    }
}
