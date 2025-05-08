using Infrastructure.Services.Assets;
using LevelDatas;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameController
    {
        private IAssetProviderService _assetProviderService;
        private Transform _objectParent;
        private SelectedLevelData _selectedLevelData;
        private SoundManager _soundManager;
        private readonly DiContainer _container;

        public GameController(IAssetProviderService assetProviderService, Transform objectParent,
            SelectedLevelData selectedLevelData, SoundManager soundManager, DiContainer container)
        {
            _soundManager = soundManager;
            _container = container;
            _selectedLevelData = selectedLevelData;
            _objectParent = objectParent;
            _assetProviderService = assetProviderService;
        }
        
        public void InitLevel()
        {
            GameObject go = _selectedLevelData.levelData.Prefab.gameObject;
            Figure.Figure fig = _assetProviderService.Instantiate<Figure.Figure>(go, _objectParent, _container);
            fig.InitFigure(_selectedLevelData.objectColor);
        }
    }
}