using System;
using Assets._Game.Scripts.Game.Objects.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Factory;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem.Producer
{
    class PooledGemProducer : IGemProducer
    {
        public PooledGemProducer(Transform container, int poolSize, IGemFactory factory)
            => throw new NotImplementedException();

        public void HandleUnused(IGem gem)
            => throw new NotImplementedException();
        public void HandleUnused(IGem[] gems)
            => throw new NotImplementedException();

        public IGem Produce()
            => throw new NotImplementedException();

        private void FiilPool()
            => throw new NotImplementedException();
    }
}
