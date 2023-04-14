using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class HealthBarHandler : IInitializable, ITickable
    {
        [Inject] private Slider _slider;
        [Inject] private HealthHolder _healthHolder;

        public void Initialize()
        {
            _slider.maxValue = _healthHolder.MaxHealth;
        }

        public void Tick()
        {
            _slider.value = _healthHolder.Health;
        }
    }
}