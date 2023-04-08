namespace Assets.Scripts
{
    public class ControllableShooter : Shooter
    {
        public override event GettingProjectile OnShoot;

        private Controls _controls;

        protected override void OnReceive()
        {
            base.OnReceive();
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Character.Shoot.performed += callbackContext => OnShoot?.Invoke
            (
                _data.ProjectileType,
                gameObject.layer,
                _shootingPoint.transform.position,
                _shootingPoint.transform.rotation
            );
        }

        private void OnDisable() => _controls.Disable();
    }
}
