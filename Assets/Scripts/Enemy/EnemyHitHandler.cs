using System;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class EnemyHitHandler : MonoBehaviour
    {
        [SerializeField] private ProjectileIntensions _hitWithIntension;
        [Inject] private HealthHolder _healthHolder;
        
        private void OnTriggerEnter(Collider other)
        {
            var projectile = other.GetComponent<ProjectileFacade>();
            Debug.LogFormat("Enemy hit with {0}", projectile);
            if (projectile != null && projectile.Intension == _hitWithIntension)
            {
                _healthHolder.TakeDamage(projectile.Damage);
                Debug.LogFormat("Enemy health changed to {0}", _healthHolder.Health);
                projectile.Dispose();
            }
        }
    }
}