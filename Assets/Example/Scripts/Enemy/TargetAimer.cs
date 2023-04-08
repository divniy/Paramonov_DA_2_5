using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TargetAimer : DataReceiver<EnemyData>
    {
        private ShootingPoint _shootingPoint;

        protected override void OnReceive()
        {
            base.OnReceive();
            _shootingPoint = GetComponentInChildren<ShootingPoint>();
        }

        public void StartAiming() => StartCoroutine(Aiming());

        private IEnumerator Aiming()
        {
            Vector3 aimOffset = Vector3.up * 1.5f;

            while (true)
            {
                transform.LookAt(new Vector3(_data.Target.transform.position.x, transform.position.y, _data.Target.transform.position.z));
                _shootingPoint.transform.LookAt(_data.Target.transform.position + aimOffset);
                yield return null;
            }
        }

        public void StopAiming() => StopAllCoroutines();
    }
}