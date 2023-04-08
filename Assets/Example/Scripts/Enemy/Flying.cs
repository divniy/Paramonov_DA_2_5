using UnityEngine;

namespace Assets.Scripts
{
    public class Flying : EnemyData
    {
        [SerializeField] private AnimationCurve _flyingCurve;

        public override EnemyTypes Type => EnemyTypes.Flying;
        public AnimationCurve FlyingCurve => _flyingCurve;
    }
}
