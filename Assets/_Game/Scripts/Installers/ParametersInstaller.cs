using Assets._Game.Scripts.Game.Input;
using Assets._Game.Scripts.Parameters.Classes;
using Assets._Game.Scripts.Parameters.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets._Game.Scripts.Parameters
{
    [CreateAssetMenu(fileName = "ParametersInstaller", menuName = "Installers/ParametersInstaller")]
    public class ParametersInstaller : ScriptableObjectInstaller<ParametersInstaller>
    {
        [SerializeField] private GameParameters _GameParameters;
        [SerializeField] private GamePrefabs _GamePrefabs;
        [SerializeField] private InputConfig _InputConfig;

        public override void InstallBindings()
        {
            BindInterfaceFromInstance<IGameParameters>(_GameParameters);
            BindInterfaceFromInstance<IGamePrefabs>(_GamePrefabs);
            BindInterfaceFromInstance<IInputConfig>(_InputConfig);

            BindInterfaceFromInstance(_GameParameters.Ball);
            BindInterfaceFromInstance(_GameParameters.Difficulty);
            BindInterfaceFromInstance(_GameParameters.Direction);
            BindInterfaceFromInstance(_GameParameters.GemGenerator);
            BindInterfaceFromInstance(_GameParameters.PlatformGenerator);
            BindInterfaceFromInstance(_GameParameters.Platform);
            BindInterfaceFromInstance(_GameParameters.Score);
        }

        private void BindInterfaceFromInstance<T>(T instance)
            => Container.BindInterfacesAndSelfTo<T>().FromInstance(instance).AsSingle();
    }
}