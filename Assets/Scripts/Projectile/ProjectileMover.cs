using System.Linq;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class ProjectileMover : ITickable
    {
        private ProjectileRegistry _registry;

        public ProjectileMover(ProjectileRegistry registry)
        {
            _registry = registry;
        }

        public void Tick()
        {
            foreach (var projectile in _registry.Projectiles.ToList())
            {
                projectile.Tick();
                if (projectile.IsExpired()) projectile.Dispose();
            }
        }
    }
}