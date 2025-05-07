using Infrastructure.Game;
using Infrastructure.Services.Assets;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class GameFactory
    {
        private readonly IAssetProviderService _assetProviderService;

        public GameFactory(IAssetProviderService assetProviderService)
        {
            _assetProviderService = assetProviderService;
        }

        public void CreateMenu()
        {
            _assetProviderService.Instantiate<GameObject>(Constants.MenuUIPath);
        }
    }
}