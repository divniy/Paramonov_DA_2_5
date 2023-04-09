using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP.Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        [Inject]
        private EnemyData _enemyData;
        [Inject]
        private Vector3 _position;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_enemyData).AsSingle();
            Container.BindInstance(_position).WhenInjectedInto<EnemyFacade>();
            // Container.Bind<EnemyFacade>().AsSingle();
            // Container.Bind<Transform>().FromComponentOnRoot();
            // Container.BindInstance(_position).WhenInjectedInto<Initializer>();
            // Container.BindInterfacesTo<Initializer>().AsSingle();
        }

        public class Initializer : IInitializable
        {
            private Transform _transform;
            private Vector3 _position;
            [Inject] private EnemyData _enemyData;
            [Inject] private EnemyFacade _enemyFacade;

            public Initializer(Transform transform, Vector3 position)
            {
                _transform = transform;
                _position = position;
            }

            public void Initialize()
            {
                SetEnemyPosition();
                Debug.LogFormat("Start enemy installer type: {0}, prefab type: {1}", _enemyData.Type, _enemyData.Prefab.GetType());
                Debug.Log(_enemyData.Prefab);
            }

            private void SetEnemyPosition()
            {
                _transform.position = _position;
            }
        }
    }
}