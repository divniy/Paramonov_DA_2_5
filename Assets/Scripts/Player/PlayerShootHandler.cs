using System.Linq;
using UnityEngine;
using Zenject;

namespace Netology.MoreAboutOOP.Player
{
    public class PlayerShootHandler : ITickable
    {
        private readonly PlayerController _player;
        private readonly ProjectileFacade.Factory _projectileFactory;
        private readonly ProjectileFacade.Settings[] _projectilSettingsArray;
        private float _lastFireTime = 0;
        private ProjectileTypes projectileType => _player.ProjectileType;
        private float fireRate => _projectilSettingsArray.First(_ => _.Type == projectileType).FireRate;

        public PlayerShootHandler(
            PlayerController _player, 
            ProjectileFacade.Factory _projectileFactory,
            ProjectileFacade.Settings[] _projectilSettingsArray)
        {
            this._player = _player;
            this._projectileFactory = _projectileFactory;
            this._projectilSettingsArray = _projectilSettingsArray;
        }

        public void Tick()
        {
            // Debug.Log("PlayerShootHandler Tick");
            // Debug.Log(_player.IsFiring);
            if (_player.IsFiring && Time.realtimeSinceStartup - _lastFireTime > fireRate)
            {
                _lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        private void Fire()
        {
            Debug.Log("PlayerShootHandler Fire()");
            _projectileFactory.Create(_player.transform, projectileType, ProjectileIntensions.Friendly);
        }
    }
}