using UnityEngine;

namespace Assets.Scripts
{
    public abstract class DataReceiver<D> : MonoBehaviour
    {
        protected D _data;

        protected void Awake() => OnReceive();

        protected virtual void OnReceive() => _data = GetComponent<D>();
    }
}
