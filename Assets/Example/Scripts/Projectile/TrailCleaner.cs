using UnityEngine;

namespace Assets.Scripts
{
    public class TrailCleaner : MonoBehaviour
    {
        private TrailRenderer _trailRenderer;

        private void Awake() => _trailRenderer = GetComponent<TrailRenderer>();

        private void OnDisable() => _trailRenderer.Clear();
    }
}
