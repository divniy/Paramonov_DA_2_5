using System.Linq;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class EnemyDeathHandler : ITickable
    {
        private readonly EnemyRegistry _registry;

        public EnemyDeathHandler(EnemyRegistry registry)
        {
            _registry = registry;
        }
        
        public void Tick()
        {
            foreach (var enemy in _registry.Enemies.ToList())
            {
                if(enemy.Health == 0) Die(enemy);
            }
        }

        private void Die(EnemyFacade enemy)
        {
            Debug.Log("Enemy died");
            enemy.Dispose();
        }
    }
}