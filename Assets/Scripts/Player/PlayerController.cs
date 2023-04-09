using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Netology.MoreAboutOOP.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerSpawnPoint _playerSpawnPoint;

        [Inject]
        public void Construct(PlayerSpawnPoint playerSpawnPoint)
        {
            _playerSpawnPoint = playerSpawnPoint;
        }
        
        [Inject] private ProjectileModel.Factory _projectileFactory;
        [Inject] private Settings _settings;
        
        public float MoveSpeed => _settings.MoveSpeed;
        public float TurnSpeed => _settings.TurnSpeed;
        
        private Vector2 _moveAmount;
        private float _turnAmount;

        private void Start()
        {
            Debug.Log("Player controller Start called");
            transform.position = _playerSpawnPoint.transform.position;
            transform.rotation = _playerSpawnPoint.transform.rotation;
        }

        private void OnEnable()
        {
            StartCoroutine(FlatMovementCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(FlatMovementCoroutine());
        }

        private void Update()
        {
            // if (Input.GetKey(KeyCode.Space))
            // {
            //     _projectileFactory.Create(ProjectileTypes.Rocket, ProjectileIntensions.Friendly);
            // }
        }
        
        private IEnumerator FlatMovementCoroutine()
        {
            while (true)
            {
                // transform.position += (transform.forward * _velocity.y + transform.right * _velocity.x) * Time.deltaTime;
                Vector3 deltaPos = transform.TransformVector(_moveAmount.x, 0, _moveAmount.y);
                transform.position += deltaPos * (MoveSpeed * Time.deltaTime);
                transform.Rotate(Vector3.up, _turnAmount * TurnSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void Move(Vector2 amount)
        {
            _moveAmount = amount;
        }

        public void Turn(float angle)
        {
            _turnAmount = angle;
        }

        public void StartFire()
        {
            Debug.Log("Start fire");
        }

        public void StopFire()
        {
            Debug.Log("Stop fire");
        }

        public class Factory : PlaceholderFactory<PlayerController>{}
        
        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
            public float TurnSpeed;
        }
    }
}