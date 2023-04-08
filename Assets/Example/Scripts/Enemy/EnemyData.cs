using UnityEngine;

namespace Assets.Scripts
{
    public abstract class EnemyData : EntityData
    {
        [SerializeField][Range(1, 10)] private float _spawnDelay;
        [SerializeField][Range(0, 100)] private float _detectionDistance;
        [SerializeField][Range(0, 10)] private int _attackRate;
        [SerializeField][Range(0, 10)] private float _stoppingDistance;

        public abstract EnemyTypes Type { get; }
        public float SpawnDelay => _spawnDelay;
        public float DetectionDistance => _detectionDistance;
        public int MaxTimeBetweenAttack => 5;
        public int AttackRate => MaxTimeBetweenAttack / _attackRate;
        public float StoppingDistance => _stoppingDistance;
        public EntityData Target { get; private set; }

        public void SetTarget(EntityData target) =>  Target = target;
    }
}
