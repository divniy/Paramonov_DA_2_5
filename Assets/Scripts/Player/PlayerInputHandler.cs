using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Netology.MoreAboutOOP.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerFacade _player;
        [Inject] private PlayerFacade.Settings _settings;
        
        public bool IsFiring { get; private set; } = false;
        
        [Inject]
        public void Construct(PlayerFacade playerFacade)
        {
            _player = playerFacade;
        }
        
        
        public void OnMove(InputAction.CallbackContext context)
        {
            var moveAmount = context.ReadValue<Vector2>();
            _player.Move(moveAmount);
            
            // Debug.LogFormat("OnMove x={0}, y={1}", moveAmount.x, moveAmount.y);
        }

        public void OnTurn(InputAction.CallbackContext context)
        {
            _player.Turn(context.ReadValue<float>());
        }
    
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started) IsFiring = true;
            if(context.canceled) IsFiring = false;
        }

        public void OnChangeWeapon(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.ProjectileType = (ProjectileTypes) context.ReadValue<float>();
            }
        }
    }
}