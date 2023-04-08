using System;
using UnityEngine;
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

        private void Start()
        {
            transform.position = _playerSpawnPoint.transform.position;
            transform.rotation = _playerSpawnPoint.transform.rotation;
        }

        public class Factory : PlaceholderFactory<PlayerController>{}
    }
}