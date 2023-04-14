using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Netology.MoreAboutOOP.Player
{
    public class PlayerFacade : MonoBehaviour, IInitializable
    {
        private PlayerSpawnPoint _playerSpawnPoint;

        [Inject]
        public void Construct(PlayerSpawnPoint playerSpawnPoint)
        {
            _playerSpawnPoint = playerSpawnPoint;
        }
        
        [Inject] private Settings _settings;
        [Inject] private HealthHolder _healthHolder;
        
        public float MoveSpeed => _settings.MoveSpeed;
        public float TurnSpeed => _settings.TurnSpeed;
        public ProjectileTypes ProjectileType
        {
            get => _settings.ProjectileType;
            set => _settings.ProjectileType = value;
        }

        private Vector2 _moveAmount;
        private float _turnAmount;
        
        public void Initialize()
        {
            _healthHolder.SetHealth(_settings.MaxHealth);
        }

        private void Start()
        {
            Debug.Log("Player controller Start called");
            Debug.LogFormat("Player health: {0}", _healthHolder.Health);
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

        public class Factory : PlaceholderFactory<PlayerFacade>{}
        
        [Serializable]
        public class Settings
        {
            public float MaxHealth;
            public float MoveSpeed;
            public float TurnSpeed;
            public ProjectileTypes ProjectileType;
        }
    }
}