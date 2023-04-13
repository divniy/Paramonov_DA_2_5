using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class ProjectileFacade : MonoBehaviour, IPoolable<Transform, ProjectileTypes, ProjectileIntensions, IMemoryPool>, IDisposable
    {
        [Inject] private Settings[] _settingsList;
        [Inject] private ProjectileMoverRegistry _registry;
        
        private Settings _settings;
        private ProjectileIntensions _intension;
        private IMemoryPool _pool;
        public float Speed => _settings.Speed;
        public float Lifetime => _settings.Lifetime;
        
        private float _spawnTime;

        public void OnSpawned(Transform tr, ProjectileTypes type, ProjectileIntensions intension, IMemoryPool pool)
        {
            _pool = pool;
            _settings = _settingsList.First(_ => _.Type == type);
            _intension = intension;
            transform.position = tr.position;
            transform.rotation = tr.rotation;
            _spawnTime = Time.realtimeSinceStartup;
            _registry.Add(this);
        }
        
        public void OnDespawned()
        {
            Debug.Log("Dispawn projectile");
            _registry.Remove(this);
            _pool = null;
        }
        
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public bool IsExpired() => Time.realtimeSinceStartup - _spawnTime > Lifetime;
        public void Tick()
        {
            transform.Translate(transform.forward * Speed * Time.deltaTime);
        }
        // private void Update() => Tick();

        public class
            BulletFactory : PlaceholderFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade> { }
        
        public class
            RocketFactory : PlaceholderFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade> { }

        public class
            Factory : PlaceholderFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade> { }
        
        [Serializable]
        public class Settings
        {
            public ProjectileTypes Type;
            public GameObject Prefab;
            public float Speed;
            public float FireRate;
            public float Lifetime;
        }
    }

    public class
        CompositeProjectileFactory : IFactory<Transform, ProjectileTypes, ProjectileIntensions, ProjectileFacade>
    {
        [Inject] private ProjectileFacade.BulletFactory _bulletFactory;
        [Inject] private ProjectileFacade.RocketFactory _rocketFactory;
        public ProjectileFacade Create(Transform param1, ProjectileTypes type, ProjectileIntensions param3)
        {
            switch (type)
            {
                case ProjectileTypes.Bullet:
                    return _bulletFactory.Create(param1, type, param3);
                case ProjectileTypes.Rocket:
                    return _rocketFactory.Create(param1, type, param3);
                default:
                    return _bulletFactory.Create(param1, type, param3);
            }
        }
    }
}