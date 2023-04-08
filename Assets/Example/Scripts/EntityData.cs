using UnityEngine;

namespace Assets.Scripts
{
    public class EntityData : MonoBehaviour, IHaveHealth, ICanMove, ICanShoot
    {
        [SerializeField][Range(1, 1000)] protected int _maxHealth;
        [SerializeField][Range(1, 10)] private float _speed;
        [SerializeField] protected ProjectileTypes _projectileType;

        public int MaxHealth => _maxHealth;
        public float Speed => _speed;
        public ProjectileTypes ProjectileType => _projectileType;
    }
}
