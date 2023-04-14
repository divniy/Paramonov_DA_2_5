using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class HealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [Inject] private HealthHolder _healthHolder;

        private void Start()
        {
            _slider.maxValue = _healthHolder.MaxHealth;
        }

        private void Update()
        {
            _slider.value = _healthHolder.Health;
        }
    }
}