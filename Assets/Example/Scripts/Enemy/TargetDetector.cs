using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class TargetDetector : DataReceiver<EnemyData>
    {
        public event Action OnDetect;
        public event Action OnLost;

        private bool _isDetection = true;

        private void Update() => Detect();

        private void Detect()
        {
            if (_data.Target != null)
            {
                if (IsDetected() && _isDetection)
                {
                    OnDetect?.Invoke();
                    _isDetection = false;
                }

                if (IsDetected() == false && _isDetection == false)
                {
                    OnLost?.Invoke();
                    _isDetection = true;
                }
            }
        }

        private bool IsDetected() => Vector3.Distance(transform.position, _data.Target.transform.position) < _data.DetectionDistance;

        private void OnDisable() => _isDetection = true;
    }
}
