using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterData : EntityData
    {
        [SerializeField][Range(0, 100)] private float _lookSensivity;

        public float LookSensivity => _lookSensivity;

        public void SwitchProjectile(ProjectileTypes type) => _projectileType = type;
    }
}
