using Netology.MoreAboutOOP.Player;
using Zenject;

namespace Netology.MoreAboutOOP
{
    public class EnemiesLookAtPlayer : ITickable
    {
        [Inject] private PlayerFacade _player;
        [Inject] private EnemyRegistry _registry;
        
        public void Tick()
        {
            foreach (var enemy in _registry.Enemies)
            {
                enemy.transform.LookAt(_player.transform);
            }
        }
    }
}