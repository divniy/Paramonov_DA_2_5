using Zenject;
using Netology.MoreAboutOOP.Player;
using UnityEngine;
using System;

namespace Netology.MoreAboutOOP.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private PlayerSettings _playerSettings;

        [Inject] 
        private EnemySpawner.Settings _enemiesSettings;
        
        public override void InstallBindings()
        {
            // Container.BindFactory<PlayerController, PlayerController.Factory>()
                // .FromComponentInNewPrefab(_playerSettings.PlayerPrefab);
                
            Container.Bind<PlayerSpawnPoint>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerController>().FromComponentInNewPrefab(_playerSettings.PlayerPrefab).AsSingle();
            Container.Bind<PlayerInputHandler>().AsSingle();

            Container.Bind<EnemySpawnPoint>().FromComponentsInHierarchy().AsTransient();
            // TODO Implement IPoolable on facade, then add IMemoryPool's behaviours here
            var commonEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Common).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.CommonEnemyFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<EnemyInstaller>(commonEnemyPrefab)
                .UnderTransformGroup("Enemies");
            
            var strongEnemyPrefab = _enemiesSettings.ForEnemyType(EnemyTypes.Strong).Prefab;
            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.StrongEnemyFactory>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<EnemyInstaller>(strongEnemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.Factory>()
                .FromFactory<CompositeEnemyFactory>();

            Container.BindFactory<ProjectileTypes, ProjectileIntensions, ProjectileModel, ProjectileModel.Factory>()
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<ProjectileInstaller>()
                .UnderTransformGroup("Projectiles");
            
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
        

        [Serializable]
        public class Settings
        {
            public int TestNumber = 3;
        }
    }
}
