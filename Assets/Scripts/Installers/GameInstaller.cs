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
            // Container.BindFactory<PlayerController, PlayerController.Factory>()
                // .FromComponentInNewPrefab(_playerSettings.PlayerPrefab);
                
            Container.Bind<PlayerSpawnPoint>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<PlayerController>().FromComponentInNewPrefab(_playerSettings.PlayerPrefab).AsSingle();
            Container.Bind<PlayerController>()
                .FromSubContainerResolve()
                .ByNewPrefabMethod(_playerSettings.PlayerPrefab, InstallPlayer)
                .AsSingle();

            Container.Bind<EnemySpawnPoint>().FromComponentsInHierarchy().AsTransient();
            // TODO Implement IPoolable on facade, then add IMemoryPool's behaviours here
            var commonEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Common).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.CommonEnemyFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(2)
                    // .FromSubContainerResolve()
                    // .ByNewPrefabInstaller<EnemyInstaller>(commonEnemyPrefab)
                    .FromComponentInNewPrefab(commonEnemyPrefab)
                    .UnderTransformGroup("Enemies"));

            var strongEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Strong).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.StrongEnemyFactory>()
                .FromMonoPoolableMemoryPool(x => x
                    .WithInitialSize(2)
                    .FromComponentInNewPrefab(strongEnemyPrefab)
                    .UnderTransformGroup("Enemies"));
            // Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.StrongEnemyFactory>()
            //     .FromSubContainerResolve()
            //     .ByNewPrefabInstaller<EnemyInstaller>(strongEnemyPrefab)
            //     .UnderTransformGroup("Enemies");

            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.Factory>()
                .FromFactory<CompositeEnemyFactory>();

            installProjectiles();
            
            /*
            Container.BindFactory<UnityEngine.Object, EnemyFacade, EnemyFacade.Factory>()
                .FromMonoPoolableMemoryPool(poolBinder => poolBinder
                        .FromFactory<PrefabFactory<EnemyFacade>>()
                        .WithInitialSize(5)
                        
                    // .UnderTransformGroup("FooPool")
                );
            */
            // Container.BindInterfacesAndSelfTo<GameInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }

        private void InstallPlayer(DiContainer subContainer)
        {
            subContainer.Bind<PlayerController>().FromComponentOnRoot().AsSingle();
            subContainer.BindInterfacesTo<PlayerShootHandler>().AsSingle();
        }

        private void installProjectiles()
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
