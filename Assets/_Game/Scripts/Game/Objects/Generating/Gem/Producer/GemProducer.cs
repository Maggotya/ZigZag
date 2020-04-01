using Assets._Game.Scripts.Game.Objects.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Factory;
using Zenject;

namespace Assets._Game.Scripts.Game.Objects.Generating.Gem.Producer
{
    class GemProducer : AbstractProducer<IGem>, IGemProducer
    {
        [Inject]
        public GemProducer(IGemFactory factory) : base(factory)
        { }
    }
}
