using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class EnemyFacade
    {
        private readonly EnemyData _enemyData;
        
        public EnemyFacade(EnemyData enemyData, Transform transform)
        {
            _enemyData = enemyData;
            Transform = transform;
        }
        
        // [Inject]
        public Transform Transform
        {
            get; private set;
        }

        public class Factory : PlaceholderFactory<EnemyData, Vector3, EnemyFacade>
        {
        }
    }
}