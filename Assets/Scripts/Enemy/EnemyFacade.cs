using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    // TODO: Implement IPoolable<IMemoryPool> for more real-world's like behaviour
    public class EnemyFacade : MonoBehaviour
    {
        private EnemyData _enemyData;
        
        [Inject]
        public void Construct(EnemyData enemyData, Vector3 position)
        {
            _enemyData = enemyData;
            transform.position = position;
        }

        private void Start()
        {
            Debug.Log("Start on EnemyFacede");
            Debug.Log(_enemyData.Serialize());
        }

        public class CommonEnemyFactory : PlaceholderFactory<EnemyData, Vector3, EnemyFacade>
        {
        }
        
        public class StrongEnemyFactory : PlaceholderFactory<EnemyData, Vector3, EnemyFacade>
        {
        }

        public class Factory : PlaceholderFactory<EnemyData, Vector3, EnemyFacade>
        {
        }
    }

    public class CompositeEnemyFactory : IFactory<EnemyData, Vector3, EnemyFacade>
    {
        private EnemyFacade.CommonEnemyFactory _commonEnemyFactory;
        private EnemyFacade.StrongEnemyFactory _strongEnemyFactory;
        
        public CompositeEnemyFactory(
            EnemyFacade.CommonEnemyFactory commonEnemyFactory, EnemyFacade.StrongEnemyFactory strongEnemyFactory)
        {
            _commonEnemyFactory = commonEnemyFactory;
            _strongEnemyFactory = strongEnemyFactory;

        }
        
        public EnemyFacade Create(EnemyData param1, Vector3 param2)
        {
            switch (param1.Type)
            {
                case EnemyTypes.Common:
                    return _commonEnemyFactory.Create(param1, param2);
                case EnemyTypes.Strong:
                    return _strongEnemyFactory.Create(param1, param2);
                default:
                    return _commonEnemyFactory.Create(param1, param2);
            }
        }
    }
}