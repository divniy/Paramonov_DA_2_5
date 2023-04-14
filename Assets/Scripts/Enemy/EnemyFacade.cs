using System;
using System.Linq;
using Netology.MoreAboutOOP.Player;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using IPoolable = Zenject.IPoolable;

namespace Netology.MoreAboutOOP
{
    // TODO: Implement IPoolable<IMemoryPool> for more real-world's like behaviour
    public class EnemyFacade : MonoBehaviour, IPoolable<EnemyData, ProjectileTypes, Vector3, IMemoryPool>, IDisposable
    {
        private EnemyData _enemyData;
        private ProjectileTypes _projectileType;
        private IMemoryPool _pool;
        [Inject] private PlayerFacade _player;
        [Inject] private HealthHolder _healthHolder;
        [Inject] private EnemyRegistry _registry;
        [Inject] private ProjectileFacade.Settings[] _projectileSettingsArray;
        
        public float MaxHealth => _enemyData.MaxHealth;
        public float Health => _healthHolder.Health;

        public float ShootingDistance => _enemyData.ShootingDistance;

        public ProjectileTypes ProjectileType => _projectileType;

        public float FireRate { get; private set; } = 0;
        public bool IsFiring { get; set; } = false;
        public float LastShot { get; set; } = 0;
        
        // [Inject]
        // public void Construct(EnemyData enemyData, Vector3 position)
        // {
            // _enemyData = enemyData;
            // transform.position = position;
        // }

        private void Start()
        {
            Debug.Log("Start on EnemyFacede");
            Debug.Log(_enemyData.Serialize());
        }

        public void OnSpawned(EnemyData enemyData, ProjectileTypes projectileType, Vector3 position, IMemoryPool pool)
        {
            _pool = pool;
            _enemyData = enemyData;
            _projectileType = projectileType;
            FireRate = _projectileSettingsArray.First(_ => _.Type == projectileType).FireRate;
            transform.position = position;
            _healthHolder.SetHealth(enemyData.MaxHealth);
            _registry.Add(this);
        }

        public void OnDespawned()
        {
            _registry.Remove(this);
            _pool = null;
        }
        
        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public class CommonEnemyFactory : PlaceholderFactory<EnemyData, ProjectileTypes, Vector3, EnemyFacade>
        {
        }
        
        public class StrongEnemyFactory : PlaceholderFactory<EnemyData, ProjectileTypes, Vector3, EnemyFacade>
        {
        }

        public class Factory : PlaceholderFactory<EnemyData, ProjectileTypes, Vector3, EnemyFacade>
        {
        }
    }

    public class CompositeEnemyFactory : IFactory<EnemyData, ProjectileTypes, Vector3, EnemyFacade>
    {
        private EnemyFacade.CommonEnemyFactory _commonEnemyFactory;
        private EnemyFacade.StrongEnemyFactory _strongEnemyFactory;
        
        public CompositeEnemyFactory(
            EnemyFacade.CommonEnemyFactory commonEnemyFactory, EnemyFacade.StrongEnemyFactory strongEnemyFactory)
        {
            _commonEnemyFactory = commonEnemyFactory;
            _strongEnemyFactory = strongEnemyFactory;
        }
        
        public EnemyFacade Create(EnemyData param1, ProjectileTypes param2, Vector3 param3)
        {
            switch (param1.Type)
            {
                case EnemyTypes.Common:
                    return _commonEnemyFactory.Create(param1, param2, param3);
                case EnemyTypes.Strong:
                    return _strongEnemyFactory.Create(param1, param2, param3);
                default:
                    return _commonEnemyFactory.Create(param1, param2, param3);
            }
        }
    }
}