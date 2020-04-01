using Assets._Game.Scripts.Game.Objects.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Factory;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Producer;
using Assets._Game.Scripts.Game.Objects.Generating.Gem.Variator;
using Assets._Game.Scripts.Parameters.Interfaces;
using Zenject;

namespace Assets._Game.Scripts.Installers
{
    class GemGeneratorInstaller : MonoInstaller<GemGeneratorInstaller>
    {
        [Inject] private IGamePrefabs _gamePrefabs { get; set; }
        [Inject] private IGameParameters _gameParameters { get; set; }

        public override void InstallBindings()
        {
            Container.BindFactory<IGem, GemFactory>().FromComponentInNewPrefab(_gamePrefabs.gem).AsSingle();
            Container.Bind<IGemFactory>().To<GemFactory>().FromResolve().AsTransient();

            Container.Bind<IGemProducer>().To<GemProducer>().AsTransient();
            Container.Bind<IVariator>().To<IVariator>().FromInstance(_variator).AsTransient();
        }

        private IVariator _variator {
            get {
                switch (_gameParameters.GemGenerator.placementType) {
                    case Parameters.Classes.PlacementType.Iterative: return _iterateVariator;
                    case Parameters.Classes.PlacementType.Random: return _randomVariator;
                    default: return _randomVariator;
                }
            }
        }

        private IterativeVariator _iterateVariator
            => new IterativeVariator(_gameParameters.GemGenerator.platformsInBlock, 1);

        private RandomVariator _randomVariator
            => new RandomVariator(_gameParameters.GemGenerator.platformsInBlock);
    }
}
