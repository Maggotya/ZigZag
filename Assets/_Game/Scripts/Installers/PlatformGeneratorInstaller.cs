using Assets._Game.Scripts.Game.Handlers.Direction;
using Assets._Game.Scripts.Game.Handlers.Probability;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Calculator.Steps;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Changer;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Factory;
using Assets._Game.Scripts.Game.Objects.Generating.Platform.Producer;
using Assets._Game.Scripts.Game.Objects.Platform;
using Assets._Game.Scripts.Parameters.Interfaces;
using Zenject;

namespace Assets._Game.Scripts.Installers
{
    public class PlatformGeneratorInstaller : MonoInstaller<PlatformGeneratorInstaller>
    {
        [Inject]
        private IGamePrefabs _gamePrefabs { get; set; }
        [Inject]
        private IGameParameters _gameParameters { get; set; }

        public override void InstallBindings()
        {
            Container.BindFactory<IPlatform, PlatformFactory>().FromComponentInNewPrefab(_gamePrefabs.platform).AsSingle();
            Container.Bind<IPlatformFactory>().To<PlatformFactory>().FromResolve().AsTransient();

            Container.Bind<IProbabilitier>().To<Probabilitier>()
                .FromInstance(_probabilitier).AsTransient();
            Container.Bind<IDirectionHandler>().To<DirectionHandler>().FromInstance(_directionHandlerInstance).AsTransient();

            Container.Bind<IDirectionChanger>().To<DirectionChangerByProbability>().AsTransient();

            Container.Bind<IStepCalculator>().To<StepCalculator>().FromInstance(_stepCalculator).AsTransient();
            Container.Bind<ILastStepDetector>().To<LastStepDetector>().AsTransient();
            Container.Bind<IPositionCalculator>().To<PositionCalculator>().AsTransient();

            Container.Bind<IPlatformProducer>().To<PlatformProducer>().AsTransient();
        }

        private Probabilitier _probabilitier
            => new Probabilitier(_gameParameters.PlatformGenerator.probabilityChangeDirection);

        private StepCalculator _stepCalculator
            => new StepCalculator(_gameParameters.Platform.size);

        private DirectionHandler _directionHandlerInstance
            => new DirectionHandler(_gameParameters.Direction.directions);
    }
}