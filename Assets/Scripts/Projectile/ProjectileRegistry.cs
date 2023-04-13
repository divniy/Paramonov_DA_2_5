using System.Collections.Generic;

namespace Netology.MoreAboutOOP
{
    public class ProjectileRegistry
    {
        private List<ProjectileFacade> _projectiles = new();

        public IEnumerable<ProjectileFacade> Projectiles => _projectiles;

        public void Add(ProjectileFacade projectile)
        {
            _projectiles.Add(projectile);
        }

        public void Remove(ProjectileFacade projectile)
        {
            _projectiles.Remove(projectile);
        }
    }
}