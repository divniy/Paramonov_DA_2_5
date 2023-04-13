using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Netology.MoreAboutOOP.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerController _playerController;
        // public bool IsFiring { get; private set; } = false;
        
        [Inject]
        public void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        
        public void OnMove(InputAction.CallbackContext context)
        {
            var moveAmount = context.ReadValue<Vector2>();
            _playerController.Move(moveAmount);
            
            // Debug.LogFormat("OnMove x={0}, y={1}", moveAmount.x, moveAmount.y);
        }

        public void OnTurn(InputAction.CallbackContext context)
        {
            _playerController.Turn(context.ReadValue<float>());
        }
    
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started) _playerController.StartFire();
            if(context.canceled) _playerController.StopFire();
        }
    }
}