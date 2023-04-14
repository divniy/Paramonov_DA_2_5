using Netology.MoreAboutOOP.Player;
using Zenject;

namespace Netology.MoreAboutOOP.Installers
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentOnRoot().AsSingle().NonLazy();
            Container.Bind<PlayerInputHandler>().FromComponentOnRoot().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
        }
    }
}