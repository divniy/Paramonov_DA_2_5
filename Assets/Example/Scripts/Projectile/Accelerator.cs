using UnityEngine;

namespace Assets.Scripts
{
    public class Accelerator : DataReceiver<ICanMove>
    {
        private void Update() => Accelerate();

        private void Accelerate() => transform.Translate(_data.Speed * Time.deltaTime * Vector3.forward);
    }
}