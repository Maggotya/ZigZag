
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Factory;
using Assets._Game.Scripts.Game.Objects.Interfaces;
using Assets._Game.Scripts.Game.Objects.Platform;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Platform.Producer
{
    class PlatformProducer : AbstractProducer<IPlatform>, IPlatformProducer
    {
        [Inject]
        public PlatformProducer(IPlatformFactory factory) : base(factory)
        { }
    }
}
