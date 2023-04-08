using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Healther : DataReceiver<IHaveHealth>
    {
        public event Action OnDead;
        public event Action<int, float> OnHealthChange;

        private float _health;

        public bool IsDead => _health <= 0;
        public int MaxHealth => _data.MaxHealth;
        public float Health
        {
            get => _health;

            private set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
                OnHealthChange?.Invoke(MaxHealth, _health);

                if (_health <= 0)
                    OnDead?.Invoke();
            }
        }

        private void OnEnable() => Restore();

        public void Kill() => Health = 0;

        public void TakeDamage(int damage) => Health -= damage;

        public void Restore() => Health = MaxHealth;
    }
}
