using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public delegate ProjectileData GettingProjectile(ProjectileTypes projectileType, int shooterLayer, Vector3 shootingPosition, Quaternion shootingRotation);

    public class EntitySpawner : MonoBehaviour
    {
        private Transform _entityContainer;
        private CharacterSpawnPoint _characterSpawnPoint;
        private EnemySpawnPoint[] _enemySpawnPoints;
        private Dictionary<ProjectileTypes, ProjectilePool> _projectiles = new();
        private Dictionary<EnemyTypes, EnemyPool<EnemyData>> _enemies;
        private CharacterData _character;

        private void Awake()
        {
            _entityContainer = FindObjectOfType<EntityContainer>().transform;
            _characterSpawnPoint = FindObjectOfType<CharacterSpawnPoint>();
            _enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        }

        private void Start()
        {
            InitProgectiles();
            SpawnCharacter();
            SpawnEnemies();
        }

        private void InitProgectiles()
        {
            _projectiles = new()
            {
                { ProjectileTypes.Bullet, new(Resources.Load<Bullet>("Prefabs/Bullet"), _entityContainer, 10) },
                { ProjectileTypes.Rocket, new(Resources.Load<Rocket>("Prefabs/Rocket"), _entityContainer, 10) },
                { ProjectileTypes.Diskette, new(Resources.Load<Diskette>("Prefabs/Diskette"), _entityContainer, 10) },
                { ProjectileTypes.FireBall, new(Resources.Load<FireBall>("Prefabs/FireBall"), _entityContainer, 10) }
            };
        }

        private CharacterData SpawnCharacter()
        {
            CharacterData prefab = Resources.Load<CharacterData>("Prefabs/Character");
            CharacterData character = Instantiate(prefab, _characterSpawnPoint.transform.position, _characterSpawnPoint.transform.rotation, _entityContainer);
            character.GetComponent<Healther>().OnHealthChange += FindObjectOfType<CharacterPanel>().GetComponentInChildren<HealthBar>().Redraw;
            character.GetComponent<ControllableShooter>().OnShoot += GetProjectile;
            _character = character;
            return character;
        }

        private void SpawnEnemies()
        {
            _enemies = new()
            {
                { EnemyTypes.Simple, new(Resources.Load<Simple>("Prefabs/SimpleEnemy"), SpawnEnemy, GetProjectile, _character, _entityContainer, 5) },
                { EnemyTypes.Flying, new(Resources.Load<Flying>("Prefabs/FlyingEnemy"), SpawnEnemy, GetProjectile, _character, _entityContainer, 5) },
                { EnemyTypes.Big, new(Resources.Load<Big>("Prefabs/BigEnemy"), SpawnEnemy, GetProjectile, _character, _entityContainer, 5) }
            };

            foreach (EnemySpawnPoint spawnPoint in _enemySpawnPoints)
                SpawnEnemy(spawnPoint);
        }

        private void SpawnEnemy()
        {
            Array types = typeof(EnemyTypes).GetEnumValues();
            EnemyTypes randomType = (EnemyTypes)types.GetValue(Random.Range(0, types.Length));
            EnemySpawnPoint randomSpawnPoint = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length)];
            _enemies[randomType].Get(randomSpawnPoint.transform.position);
        }

        private void SpawnEnemy(EnemySpawnPoint spawnPoint)
        {
            Array types = typeof(EnemyTypes).GetEnumValues();
            EnemyTypes randomType = (EnemyTypes)types.GetValue(Random.Range(0, types.Length));
            _enemies[randomType].Get(spawnPoint.transform.position);
        }

        private ProjectileData GetProjectile(ProjectileTypes projectileType, int shooterLayer, Vector3 shootingPosition, Quaternion shootingRotation)
        {
            ProjectileData projectile = _projectiles[projectileType].Get(shootingPosition, shootingRotation);
            projectile.GetComponent<Hitter>().SetShooterLayer(shooterLayer);
            return projectile;
        }
    }
}
