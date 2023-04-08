using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Entities.Character
{
    [RequireComponent(typeof(CharacterController), typeof(CharacterData))]
    public class FirstPersonController : DataReceiver<CharacterData>
    {
        private Controls _controls;
        private CharacterController _controller;
        private Coroutine _motionRoutine;
        private float _gravity = 10;
        private Camera _lookingCamera;
        private float _rotationX;

        protected override void OnReceive()
        {
            base.OnReceive();
            _controls = new();
            _controller = GetComponent<CharacterController>();
            _lookingCamera = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Character.Motion.started += callbackContext => _motionRoutine = StartCoroutine(Motion(callbackContext));
            _controls.Character.Motion.canceled += callbackContext => StopCoroutine(_motionRoutine);
            _controls.Character.Lookation.performed += callbackContext => Look(callbackContext);
        }

        private void Update() => Gravitate();

        private IEnumerator Motion(InputAction.CallbackContext callbackContext)
        {
            while (true)
            {
                Vector2 motionInput = callbackContext.ReadValue<Vector2>();
                _controller.Move(transform.TransformDirection(_data.Speed * Time.deltaTime * new Vector3(motionInput.x, 0, motionInput.y)));
                yield return null;
            }
        }

        private void Gravitate()
        {
            if (_controller.isGrounded == false)
                _controller.Move(_gravity * Time.deltaTime * Vector3.down);
        }

        private void Look(InputAction.CallbackContext callbackContext)
        {
            float lookAngle = 90;
            Vector2 lookingInputDelta = callbackContext.ReadValue<Vector2>();
            Vector3 lookation = new(-lookingInputDelta.y, lookingInputDelta.x);
            _rotationX += lookation.x * _data.LookSensivity * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -lookAngle, lookAngle);
            _lookingCamera.transform.localRotation = Quaternion.Euler(_rotationX * Vector3.right);
            transform.Rotate(_data.LookSensivity * Time.deltaTime * lookation.y * Vector3.up);
        }

        private void OnDisable() => _controls.Disable();
    }
}