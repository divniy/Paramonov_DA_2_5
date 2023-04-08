using System.Collections;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class AIController : DataReceiver<EnemyData>
    {
        private NavMeshAgent _navigationAgent;

        protected override void OnReceive()
        {
            base.OnReceive();
            _navigationAgent = GetComponent<NavMeshAgent>();
        }

        public void StartFollowing() => StartCoroutine(Following());

        private IEnumerator Following()
        {
            while (true)
            {
                _navigationAgent.SetDestination(_data.Target.transform.position);
                yield return null;
            }
        }

        public void StopFollowing() => StopAllCoroutines();
    }
}
