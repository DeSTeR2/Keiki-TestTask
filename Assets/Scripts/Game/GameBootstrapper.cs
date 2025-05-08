using System;
using Infrastructure.Services.Assets;
using LevelDatas;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Transform _objectParent;
        private SelectedLevelData _selectedLevel;
        private IAssetProviderService _assetProviderService;
        private GameController _gameController;

        [Inject]
        public void Construct(GameController gameController)
        {
            _gameController = gameController;
        }

        private void Awake()
        {
            InitLevel();
        }

        private void InitLevel()
        {
            _gameController.InitLevel();
        }

        private void OnDestroy()
        {
            _gameController.Dispose();
        }
    }
}