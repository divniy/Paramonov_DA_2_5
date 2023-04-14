using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Netology.MoreAboutOOP.Player;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class EnemiesShootHandler : ITickable
    {
        private readonly EnemyRegistry _registry;
        private readonly PlayerFacade _player;
        private readonly ProjectileFacade.Factory _projectileFactory;

        public EnemiesShootHandler(EnemyRegistry registry, PlayerFacade player, 
            ProjectileFacade.Factory projectileFactory)
        {
            _registry = registry;
            _player = player;
            _projectileFactory = projectileFactory;
        }

        public void Tick()
        {
            foreach (var enemy in _registry.Enemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, _player.transform.position);
                enemy.IsFiring = distance <= enemy.ShootingDistance;
                if (enemy.IsFiring && Time.realtimeSinceStartup - enemy.LastShot > enemy.FireRate)
                {
                    enemy.LastShot = Time.realtimeSinceStartup;
                    Fire(enemy);
                }
                // Debug.LogFormat("{0} distance to player: {1}", enemy, distance);
            }
        }

        private void Fire(EnemyFacade enemy)
        {
            _projectileFactory.Create(enemy.transform, enemy.ProjectileType, ProjectileIntensions.Hostile);
        }
    }
}