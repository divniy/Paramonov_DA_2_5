using System.Collections.Generic;

namespace Netology.MoreAboutOOP
{
    public class EnemyRegistry
    {
        private List<EnemyFacade> _enemies = new();

        public IEnumerable<EnemyFacade> Enemies => _enemies;

        public void Add(EnemyFacade enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyFacade enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}