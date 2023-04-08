using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectilePool : Pool<ProjectileData>
    {
        public ProjectilePool(ProjectileData prefab, Transform parent, int count = 1) : base(prefab, parent) 
        {
            Init(count);
        }

        protected override ProjectileData GetCreated() => Object.Instantiate(_prefab);
    }
}
