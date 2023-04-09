using UnityEngine;
using Zenject;
using System;

namespace Netology.MoreAboutOOP
{
    public class ProjectileModel : IInitializable
    {
        [Inject] private ProjectileTypes _projectileType;
        [Inject] private ProjectileIntensions _projectileIntension;
        [Inject] private ISettings _settings;

        public ProjectileTypes Type => _projectileType;
        public ProjectileIntensions Intension => _projectileIntension;
        public float Speed => _settings.Speed;
        public float Damage => _settings.Damage;
        public float FireRate => _settings.FireRate;
        public float Lifetime => _settings.Lifetime;

        [Inject]
        public Transform Transform
        {
            get; private set;
        }
        
        public void Initialize()
        {
            Debug.Log("Initialize ProjectileModel");
        }


        /*public class BulletFactory : BaseFactory
        {
            
        }
        
        public class RocketFactory : BaseFactory
        {
            
        }
        
        public class Factory : BaseFactory
        {
            
        }
        
        public abstract class BaseFactory : PlaceholderFactory<ProjectileTypes, ProjectileIntensions, ProjectileFacade>
        {
        }*/
        public class Factory : PlaceholderFactory<ProjectileTypes, ProjectileIntensions, ProjectileModel>
        {
        }

        public interface ISettings
        {
            float Speed { get; }
            float Damage { get; }
            float FireRate { get; }
            float Lifetime { get; }
        }
    }
}