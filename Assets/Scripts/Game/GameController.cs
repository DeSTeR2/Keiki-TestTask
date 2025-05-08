using System;
using Infrastructure.Events;
using Infrastructure.Services.Assets;
using LevelDatas;
using UnityEngine;
using Zenject;
using EventType = Infrastructure.Events.EventType;

namespace Game
{
    public class GameController : IDisposable
    {
        private IAssetProviderService _assetProviderService;
        private Transform _objectParent;
        private SelectedLevelData _selectedLevelData;
        private SoundManager _soundManager;
        private readonly DiContainer _container;
        private readonly AllEvents _allEvents;
        private Figure.Figure _fig;
        private AllEvents _gameEvent;
        private EventHolder _gameSceneLoadedEvent;

        public GameController(IAssetProviderService assetProviderService, Transform objectParent,
            SelectedLevelData selectedLevelData, SoundManager soundManager, DiContainer container, AllEvents allEvents)
        {
            _soundManager = soundManager;
            _container = container;
            _allEvents = allEvents;
            _selectedLevelData = selectedLevelData;
            _objectParent = objectParent;
            _assetProviderService = assetProviderService;
        }
        
        public void InitLevel()
        {
            GameObject go = _selectedLevelData.levelData.Prefab.gameObject;
            _fig = _assetProviderService.Instantiate<Figure.Figure>(go, _objectParent, _container);

            _gameSceneLoadedEvent = _allEvents[EventType.GameSceneLoaded];
            _gameSceneLoadedEvent.Event += InitFigure;
        }

        private void InitFigure()
        {
            _fig.InitFigure(_selectedLevelData.objectColor);
        }

        public void Dispose()
        {
            _gameSceneLoadedEvent.Event -= InitFigure;
        }
    }
}