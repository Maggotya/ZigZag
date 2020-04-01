using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Game.Scripts.Game;
using Assets._Game.Scripts.Game.Save;
using Zenject;

namespace Assets._Game.Scripts.Installers
{
    class ClassInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveManager>().To<SaveManager>().AsSingle();
            Container.Bind<ITimeScaleManager>().To<TimeScaleManager>().AsSingle();
        }
    }
}
