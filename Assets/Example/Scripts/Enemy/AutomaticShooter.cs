using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AutomaticShooter : Shooter
    {
        public override event GettingProjectile OnShoot;

        private EnemyData _enemyData;
        private WaitForSeconds _waitAttackRate;

        private void Start()
        {
            _enemyData = _data as EnemyData;
            _waitAttackRate = new WaitForSeconds(_enemyData.AttackRate);
        }

        public void StartShooting() => StartCoroutine(Shooting());

        private IEnumerator Shooting()
        {
            while (true)
            {
                OnShoot?.Invoke(_data.ProjectileType, gameObject.layer, _shootingPoint.transform.position, _shootingPoint.transform.rotation);
                yield return _waitAttackRate;
            }
        }

        public void StopShooting() => StopAllCoroutines();
    }
}
