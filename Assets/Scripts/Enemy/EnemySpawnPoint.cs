using UnityEngine;

namespace Netology.MoreAboutOOP
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private EnemyTypes _enemyType;
        [SerializeField] private ProjectileTypes _projectileType;
        public EnemyTypes EnemyType => _enemyType;
        public ProjectileTypes ProjectileType => _projectileType;
    }
}