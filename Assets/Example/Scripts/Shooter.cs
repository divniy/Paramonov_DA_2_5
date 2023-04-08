namespace Assets.Scripts
{
    public abstract class Shooter : DataReceiver<ICanShoot>
    {
        public abstract event GettingProjectile OnShoot;

        protected ShootingPoint _shootingPoint;

        protected override void OnReceive()
        {
            base.OnReceive();
            _shootingPoint = GetComponentInChildren<ShootingPoint>();
        }
    }
}
