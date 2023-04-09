using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP.Installers
{
    public class ProjectileInstaller : Installer<ProjectileInstaller>
    {
        [Inject] private ProjectileTypes _projectileType;
        [Inject] private ProjectileIntensions _intension;
        [Inject] private Settings[] _settingsList;
        // [Inject] private Settings _settings;
        public override void InstallBindings()
        {
            Container.BindInstance(_projectileType);
            Container.BindInstance(_intension).AsSingle();
            Container.BindInstance(_settingsList.First(_ => _.Type == _projectileType));
            Container.Bind<Transform>().FromComponentOnRoot();
            Container.BindInterfacesAndSelfTo<ProjectileModel>().AsSingle();
            Container.Bind<ProjectileModel.ISettings>().To<Settings>().WhenInjectedInto<ProjectileModel>();
        }
        
        [Serializable]
        public class Settings : ProjectileModel.ISettings
        {
            public ProjectileTypes Type;
            public GameObject ViewPrefab;
            public float _speed;
            public float _damage;
            public float _fireRate;
            public float _lifetime;

            public float Speed => _speed;

            public float Damage => _damage;

            public float FireRate => _fireRate;

            public float Lifetime => _lifetime;
        }
    }
}