using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public interface IHealthOwner
    {
        float MaxHealth { get; }
    }
    
    public class HealthHolder
    {
        private float _health;
        
        public float Health => _health;
        public float MaxHealth { get; private set; }

        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(0f, _health - damage);
        }

        public void SetHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            _health = maxHealth;
        }
    }
}