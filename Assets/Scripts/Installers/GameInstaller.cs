using Zenject;
using System;
using Netology.MoreAboutOOP.Player;
using UnityEngine;

namespace Netology.MoreAboutOOP.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        private PlayerSettings _playerSettings;
        
        public override void InstallBindings()
        {
            Container.BindFactory<PlayerController, PlayerController.Factory>()
                .FromComponentInNewPrefab(_playerSettings.PlayerPrefab);
            Container.Bind<EnemySpawnPoint>().FromComponentsInHierarchy().AsTransient();


            Container.BindFactory<EnemyData, Vector3, EnemyFacade, EnemyFacade.Factory>()
                .FromSubContainerResolve()
                .ByNewGameObjectInstaller<EnemyInstaller>()
                .WithGameObjectName("Enemy")
                .UnderTransformGroup("Enemies");
            /*
            Container.BindFactory<UnityEngine.Object, EnemyFacade, EnemyFacade.Factory>()
                .FromMonoPoolableMemoryPool(poolBinder => poolBinder
                        .FromFactory<PrefabFactory<EnemyFacade>>()
                        .WithInitialSize(5)
                        
                    // .UnderTransformGroup("FooPool")
                );
            */
            // Container.BindFactory<UnityEngine.Object, EnemyFacade, EnemyFacade.Factory>()
            // .FromFactory<PrefabFactory<EnemyFacade>>();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }
        

        [Serializable]
        public class Settings
        {
            public int TestNumber = 3;
        }
    }
}
