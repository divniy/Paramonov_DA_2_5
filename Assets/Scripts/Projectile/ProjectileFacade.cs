using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class ProjectileFacade : MonoBehaviour, IPoolable<Transform, ProjectileTypes, ProjectileIntensions, IMemoryPool>, IDisposable
    {
        [Inject] private Settings[] _settingsList;
        [Inject] private ProjectileRegistry _registry;
        
        private Settings _settings;
        private ProjectileIntensions _intension;
        private IMemoryPool _pool;
        public float Speed => _settings.Speed;
        public float Lifetime => _settings.Lifetime;
        public float Damage => _settings.Damage;

        public ProjectileIntensions Intension => _intension;
        
        private float _spawnTime;

        public void OnSpawned(Transform tr, ProjectileTypes type, ProjectileIntensions intension, IMemoryPool pool)
        {
            _pool = pool;
            _settings = _settingsList.First(_ => _.Type == type);
            _intension = intension;
            transform.position = tr.position;
            transform.rotation = tr.rotation;
            // transform.position += transform.forward + transform.up;
            var offset = new Vector3(0, 1, 1);
            transform.Translate(offset);
            
            _spawnTime = Time.realtimeSinceStartup;
            // Debug.LogFormat("Spawn bullet pos {0} rot {1}", transform.position, transform.eulerAngles);
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
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            // transform.position += transform.forward * Speed * Time.deltaTime;
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
            public float Damage;
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