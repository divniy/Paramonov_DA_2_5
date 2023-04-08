using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private Slider _slider;

        private void Awake() => _slider = GetComponentInChildren<Slider>();

        public void Redraw(int maxHealth, float health) => _slider.value = health > 0 ? health / maxHealth : 0;
    }
}