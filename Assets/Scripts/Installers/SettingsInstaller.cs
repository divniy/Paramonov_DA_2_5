using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        public GameInstaller.Settings MainSettings;
        public Player.PlayerSettings PlayerSettings;
        public EnemySpawner.Settings EnemySettings;
        public override void InstallBindings()
        {
            Container.BindInstance(MainSettings);
            Container.BindInstance(PlayerSettings);
            Container.BindInstance(EnemySettings);
        }
    }
}
