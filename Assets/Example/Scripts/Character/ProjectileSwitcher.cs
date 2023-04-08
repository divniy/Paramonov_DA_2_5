using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class ProjectileSwitcher : DataReceiver<CharacterData>
    {
        private ProjectileTypes[] _projectiles = 
        {
            ProjectileTypes.Bullet,
            ProjectileTypes.Rocket,
            ProjectileTypes.Diskette
        };

        private int _currentProjectileIndex;
        private Controls _controls;

        protected override void OnReceive()
        {
            base.OnReceive();
            _controls = new();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Character.ProjectileScroll.performed += callbackContext => Switch(callbackContext);
        }

        private void Switch(InputAction.CallbackContext callbackContext)
        {
            _currentProjectileIndex += (int)callbackContext.ReadValue<Vector2>().y;
            _currentProjectileIndex = Mathf.Clamp(_currentProjectileIndex, 0, _projectiles.Length - 1);
            _data.SwitchProjectile(_projectiles[_currentProjectileIndex]);
        }

        private void OnDisable() => _controls.Disable();
    }
}
