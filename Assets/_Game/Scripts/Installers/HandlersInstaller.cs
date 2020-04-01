using Assets._Game.Scripts.Game.Handlers.Direction;
using Assets._Game.Scripts.Game.Handlers.Speed;
using Assets._Game.Scripts.Parameters.Interfaces;
using Zenject;

namespace Assets._Game.Scripts.Installers
{
    public class HandlersInstaller : MonoInstaller<HandlersInstaller>
    {
        [Inject]
        private IGameParameters _GameParameters { get; set; }

        public override void InstallBindings()
        {
            Container.Bind<ISpeedHandler>().To<ISpeedHandler>().FromInstance(_speedHandlerInstance).AsTransient();
            Container.Bind<IDirectionHandler>().To<IDirectionHandler>().FromInstance(_directionHandlerInstance).AsTransient();
        }

        private ISpeedHandler _speedHandlerInstance
            => new SpeedHandler(
                    _GameParameters.Ball.startSpeed,
                    _GameParameters.Ball.acceleration,
                    _GameParameters.Ball.minSpeed,
                    _GameParameters.Ball.maxSpeed);

        private IDirectionHandler _directionHandlerInstance
            => new DirectionHandler(_GameParameters.Direction.directions);
    }
}