using System;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Factory;
using Assets._Game.Scripts.Game.Objects.Platform;
using UnityEngine;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Producer
{
    class PooledPlatformProducer : IPlatformProducer
    {
        public PooledPlatformProducer(Transform container, int poolSize, IPlatformFactory factory)
            => throw new NotImplementedException();

        public void HandleUnused(IPlatform platform)
            => throw new NotImplementedException();
        public void HandleUnused(IPlatform[] platforms)
            => throw new NotImplementedException();

        public IPlatform Produce()
            => throw new NotImplementedException();

        private void FiilPool()
            => throw new NotImplementedException();
    }
}
