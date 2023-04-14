using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using IInitializable = Zenject.IInitializable;

namespace Netology.MoreAboutOOP
{
    public class EnemySpawner : IInitializable
    {
        private List<EnemySpawnPoint> _enemySpawnPoints;
        private EnemyFacade.Factory _enemyFactory;
        private Settings _settings;

        public EnemySpawner(List<EnemySpawnPoint> enemySpawnPoints, EnemyFacade.Factory enemyFactory, Settings settings)
        {
            _enemySpawnPoints = enemySpawnPoints;
            _enemyFactory = enemyFactory;
            _settings = settings;
        }
        
        public void Initialize()
        {
            // Debug.Log(_enemyPrefabs);
            foreach (EnemySpawnPoint enemySpawnPoint in _enemySpawnPoints)
            {
                // Debug.Log(_enemyPrefabs[enemySpawnPoint.EnemyType]);
                var enemyData = _settings.ForEnemyType(enemySpawnPoint.EnemyType);
                var projectileType = enemySpawnPoint.ProjectileType;
                var enemyPosition = enemySpawnPoint.transform.position;
                Debug.LogFormat("Before spawn enemy with: {0}", enemyData);
                var enemy = _enemyFactory.Create(enemyData, projectileType, enemyPosition);
                Debug.LogFormat("After spawn enemy: {0}", enemy);
                // enemy.Transform.position = enemySpawnPoint.transform.position;
                Debug.LogFormat("After set enemy position {0}", enemy.transform.position);
            }
        }

        [Serializable]
        public class Settings
        {
            public EnemyData[] Enemies;

            public EnemyData ForEnemyType(EnemyTypes enemyType)
            {
                return Enemies.First(_ => _.Type == enemyType);
            }
        }
    }
}