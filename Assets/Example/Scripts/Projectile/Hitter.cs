using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hitter : DataReceiver<ProjectileData>
    {
        public event Action OnHit;

        private int _shooterLayer;

        public void SetShooterLayer(int shooterLayer) => _shooterLayer = shooterLayer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Healther healthder) && healthder.gameObject.layer != _shooterLayer)
                healthder.TakeDamage(_data.Damage);

            OnHit?.Invoke();
            gameObject.SetActive(false);
        }
    }
}