using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyPool<E> : Pool<E> where E : EnemyData
    {
        private Action _spawning;
        private GettingProjectile _gettingProjectile;
        private EntityData _target;

        public EnemyPool(E prefab, Action spawning, GettingProjectile gettingProjectile, EntityData target, Transform parent, int count) : base(prefab, parent) 
        {
            _spawning = spawning;
            _gettingProjectile = gettingProjectile;
            _target = target;
            Init(count);
        }

        protected override E GetCreated()
        {
            E enemy = UnityEngine.Object.Instantiate(_prefab);
            enemy.SetTarget(_target);

            switch (_prefab.Type)
            {
                case EnemyTypes.Flying:
                    break;
                case EnemyTypes.Simple:
                    break;
                case EnemyTypes.Big:
                    break;
            }

            Healther healther = enemy.GetComponent<Healther>();
            TargetDetector targetDetector = enemy.GetComponent<TargetDetector>();
            AIController aIController = enemy.GetComponent<AIController>();
            TargetAimer targetAimer = enemy.GetComponent<TargetAimer>();
            AutomaticShooter automaticShooter = enemy.GetComponent<AutomaticShooter>();
            healther.OnHealthChange += enemy.GetComponentInChildren<HealthBar>().Redraw;
            healther.OnDead += () => enemy.gameObject.SetActive(false);
            healther.OnDead += _spawning;
            targetDetector.OnDetect += aIController.StartFollowing;
            targetDetector.OnLost += aIController.StopFollowing;
            targetDetector.OnDetect += targetAimer.StartAiming;
            targetDetector.OnLost += targetAimer.StopAiming;
            targetDetector.OnDetect += automaticShooter.StartShooting;
            targetDetector.OnLost += automaticShooter.StopShooting;
            automaticShooter.OnShoot += _gettingProjectile;
            return enemy;
        }
    }
}
