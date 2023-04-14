using Netology.MoreAboutOOP.Player;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public GameInstaller.Settings MainSettings;
        public Player.PlayerSettings PlayerSettings;
        public PlayerFacade.Settings PlayerControllerSettings;
        public EnemySpawner.Settings EnemySettings;
        // public ProjectileInstaller.Settings[] ProjectileSettings;
        public ProjectileFacade.Settings[] Projectiles;
        public override void InstallBindings()
        {
            Container.BindInstance(MainSettings);
            Container.BindInstance(PlayerSettings);
            Container.BindInstance(PlayerControllerSettings);
            Container.BindInstance(EnemySettings);
            // Container.BindInstance(ProjectileSettings);
            Container.BindInstance(Projectiles);
        }
    }
}
