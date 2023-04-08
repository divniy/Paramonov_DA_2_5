using UnityEngine;
namespace Assets.Scripts
{
    public class Bilborder : MonoBehaviour
    {
        private Transform _target;

        private void Start() => _target = GetComponentInParent<EnemyData>().Target.transform;

        private void Update() => transform.LookAt(_target.position);
    }
}
