using UnityEngine;

namespace Assets.Scripts
{
    public class LifeTimer : DataReceiver<IHaveLifetime>
    {
        private float _currentLifetime;

        private void Update() => Live();

        private void ResetLifetime() => _currentLifetime = default;

        private void Live()
        {
            _currentLifetime += Time.deltaTime;

            if (_currentLifetime >= _data.Lifetime)
                gameObject.SetActive(false);
        }

        private void OnDisable() => ResetLifetime();
    }
}
