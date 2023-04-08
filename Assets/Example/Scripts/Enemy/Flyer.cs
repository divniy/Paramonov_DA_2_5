using UnityEngine;

namespace Assets.Scripts
{
    public class Flyer : DataReceiver<Flying>
    {
        private float _currentAnimationTime, _animationTimeLimit;

        protected override void OnReceive()
        {
            base.OnReceive();
            _animationTimeLimit = _data.FlyingCurve.keys[_data.FlyingCurve.keys.Length - 1].time;
            _currentAnimationTime = Random.Range(0, _animationTimeLimit);
        }

        private void Update() => Fly();

        private void Fly()
        {
            transform.position = new Vector3(transform.position.x, _data.FlyingCurve.Evaluate(_currentAnimationTime), transform.position.z);
            _currentAnimationTime += Time.deltaTime;

            if (_currentAnimationTime >= _animationTimeLimit)
                _currentAnimationTime = 0;
        }
    }
}
