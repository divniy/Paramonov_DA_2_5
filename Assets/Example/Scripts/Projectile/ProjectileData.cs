using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Hitter), typeof(Accelerator), typeof(LifeTimer))]
    [RequireComponent(typeof(TrailCleaner))]
    public abstract class ProjectileData : MonoBehaviour, ICanMove, IHaveLifetime
    {
        [SerializeField][Range(0, 1000)] private int _damage;
        [SerializeField][Range(1, 100)] private float _speed;
        [SerializeField][Range(1, 10)] private float _lifetime;

        public abstract ProjectileTypes Type { get; }
        public int Damage => _damage;
        public float Speed => _speed;
        public float Lifetime => _lifetime;
    }
}
