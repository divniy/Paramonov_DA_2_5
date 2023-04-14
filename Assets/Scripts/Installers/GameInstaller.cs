using Zenject;
using Netology.MoreAboutOOP.Player;
using UnityEngine;
using System;
using System.Linq;

namespace Netology.MoreAboutOOP.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private PlayerSettings _playerSettings;

        [Inject] 
        private EnemySpawner.Settings _enemiesSettings;

        [Inject] private ProjectileFacade.Settings[] _projectileSettings;
        
        public override void InstallBindings()
        {
            InstallPlayer();

            InstallEnemies();

            InstallProjectiles();
        }

        void InstallPlayer()
        {
            Container.Bind<PlayerSpawnPoint>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<PlayerController>().FromComponentInNewPrefab(_playerSettings.PlayerPrefab).AsSingle();
            Container.Bind<PlayerController>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerSettings.PlayerPrefab)
                .AsSingle();
        }

        void InstallEnemies()
        {
            Container.Bind<EnemyRegistry>().AsSingle().NonLazy();

            Container.BindInterfacesTo<EnemyDeathHandler>().AsSingle().NonLazy();

            Container.BindInterfacesTo<EnemiesLookAtPlayer>().AsSingle().NonLazy();

            Container.Bind<EnemySpawnPoint>().FromComponentsInHierarchy().AsTransient();

            var commonEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Common).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.CommonEnemyFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(2)
                    .FromSubContainerResolve()
                    // .ByNewPrefabInstaller<EnemyInstaller>(commonEnemyPrefab)
                    // .FromComponentInNewPrefab(commonEnemyPrefab)
                    .ByNewPrefabInstaller<EnemyInstaller>(commonEnemyPrefab)
                    .UnderTransformGroup("Enemies"));

            var strongEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Strong).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.StrongEnemyFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(2)
                    // .FromComponentInNewPrefab(strongEnemyPrefab)
                    .FromSubContainerResolve()
                    // .ByNewPrefabMethod(strongEnemyPrefab, InstallEnemy)
                    .ByNewPrefabInstaller<EnemyInstaller>(strongEnemyPrefab)
                    .UnderTransformGroup("Enemies"));
            // Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.StrongEnemyFactory>()
            //     .FromSubContainerResolve()
            //     .ByNewPrefabInstaller<EnemyInstaller>(strongEnemyPrefab)
            //     .UnderTransformGroup("Enemies");

            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.Factory>()
                .FromFactory<CompositeEnemyFactory>();
            
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }

        void InstallProjectiles()
        {
            Container.Bind<ProjectileRegistry>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileMover>().AsSingle().NonLazy();
            
            Container
                .BindFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade,
                    ProjectileFacade.BulletFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(10)
                    .FromNewComponentOnNewPrefab(_projectileSettings.First(_ => _.Type == ProjectileTypes.Bullet).Prefab)
                    .UnderTransformGroup("Projectiles")
                );
            
            Container
                .BindFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade,
                    ProjectileFacade.RocketFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(5)
                    .FromNewComponentOnNewPrefab(_projectileSettings.First(_ => _.Type == ProjectileTypes.Rocket).Prefab)
                    .UnderTransformGroup("Projectiles")
                );

            Container.BindFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade,
                ProjectileFacade.Factory>().FromFactory<CompositeProjectileFactory>();
        }
        

        [Serializable]
        public class Settings
        {
            public int TestNumber = 3;
        }
    }
}
