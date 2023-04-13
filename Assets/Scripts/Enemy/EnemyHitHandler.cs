using System;
using UnityEngine;

namespace Netology.MoreAboutOOP
{
    public class EnemyHitHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var projectile = other.GetComponent<ProjectileFacade>();
            Debug.LogFormat("Enemy hit with {0}", projectile);
            if (projectile != null)
            {
                projectile.Dispose();
            }
        }
    }
}