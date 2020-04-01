using Assets._Game.Scripts.Game.Objects.Ball;
using Assets._Game.Scripts.Game.Objects.Generating.Gem;
using Assets._Game.Scripts.Game.Objects.Generating.Platform;
using Assets._Game.Scripts.Game.Scoring;
using Assets._Game.Scripts.UI.Manager;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Installers
{
    class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _Ball;
        [SerializeField] private GameObject _VirtualCamera;
        [SerializeField] private GameObject _PlatformGenerator;
        [SerializeField] private GameObject _GemGenerator;
        [SerializeField] private GameObject _Score;
        [SerializeField] private GameObject _UiManager;

        public override void InstallBindings()
        {
            Container.Bind<IGemGenerator>().To<GemGenerator>().FromComponentOn(_GemGenerator).AsSingle();
            Container.Bind<IPlatformGenerator>().To<PlatformGenerator>().FromComponentOn(_PlatformGenerator).AsSingle();
            Container.Bind<IBall>().To<BallObject>().FromComponentOn(_Ball).AsSingle();
            Container.Bind<CinemachineVirtualCamera>().FromComponentOn(_VirtualCamera).AsSingle();
            Container.Bind<IScore>().To<Score>().FromComponentOn(_Score).AsSingle();
            Container.Bind<IUiManager>().To<UiManager>().FromComponentOn(_UiManager).AsSingle();
        }
    }
}
