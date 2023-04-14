using Zenject;
using Netology.MoreAboutOOP.Player;

namespace Netology.MoreAboutOOP
{
    public class GameInitializer : IInitializable
    {
        private PlayerFacade.Factory _playerFactory;
        private PlayerFacade _playerFacade;
        
        public GameInitializer(PlayerFacade.Factory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _playerFacade = _playerFactory.Create();
        }
    }
}