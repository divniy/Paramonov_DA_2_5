using Zenject;
using Netology.MoreAboutOOP.Player;

namespace Netology.MoreAboutOOP
{
    public class GameInitializer : IInitializable
    {
        private PlayerController.Factory _playerFactory;
        private PlayerController _playerController;
        
        public GameInitializer(PlayerController.Factory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _playerController = _playerFactory.Create();
        }
    }
}