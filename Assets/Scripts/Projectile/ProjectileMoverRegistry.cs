using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class ProjectileMoverRegistry : ITickable
    {
        private List<ProjectileFacade> _projectiles = new();

        public void Add(ProjectileFacade projectile)
        {
            _projectiles.Add(projectile);
        }

        public void Remove(ProjectileFacade projectile)
        {
            _projectiles.Remove(projectile);
        }

        public void Tick()
        {
            foreach (var projectile in _projectiles.ToList())
            {
                projectile.Tick();
                if (projectile.IsExpired()) projectile.Dispose();
            }
        }
    }
}