using System;
using UnityEngine;

namespace Netology.MoreAboutOOP
{
    [Serializable]
    public class EnemyData
    {
        public EnemyTypes Type;
        public GameObject Prefab;
        public float MaxHealth = 100;
    }
}