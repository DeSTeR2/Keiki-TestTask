using System;
using System.Threading.Tasks;
using Game.Character;
using Game.Tips;
using Infrastructure.Events;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using Infrastructure.StateMachine;
using LevelDatas;
using Systems;
using UnityEngine;
using Zenject;
using EventType = Infrastructure.Events.EventType;

namespace Game
{
    public class GameController : IDisposable
    {
        private readonly DiContainer _container;
        private readonly AllEvents _allEvents;
        private readonly GameStateMachine _gameState;
        private readonly GameConfig _gameConfig;

        private IAssetProviderService _assetProviderService;
        private Transform _objectParent;
        private readonly LevelDataCollection _levelDataCollection;
        private SoundManager _soundManager;
        private Figure.Figure _fig;
        private AllEvents _gameEvent;
        private EventHolder _gameSceneLoadedEvent;

        private EventHolder _levelCleared;
        private Ufo _character;
        private ArrowTip _arrowTip;
        
        private bool isWin = false;
        private TipsController _tipsController;

        public GameController(IAssetProviderService assetProviderService, Transform objectParent, LevelDataCollection levelDataCollection, 
            SoundManager soundManager, DiContainer container, AllEvents allEvents, 
            GameStateMachine gameState, GameConfig gameConfig)
        {
            _soundManager = soundManager;
            _container = container;
            _allEvents = allEvents;
            _gameState = gameState;
            _gameConfig = gameConfig;
            _objectParent = objectParent;
            _levelDataCollection = levelDataCollection;
            _assetProviderService = assetProviderService;
        }

        public void Dispose()
        {
            _gameSceneLoadedEvent.Event -= InitFigure;
            _gameSceneLoadedEvent.Event -= PlayStartSound;
            _levelCleared.Event -= Win;
            
            _tipsController.Dispose();
        }

        public void InitLevel()
        {
            Spawn();
            SubscribeToEvents();
        }

        private void Spawn()
        {
            SpawnFigure();
            SpawnCharacter();
            SpawnArrowTip();

            _fig.SetCharacter(_character);
        }

        private void SpawnArrowTip()
        {
            _arrowTip = _assetProviderService.Instantiate<ArrowTip>(Constants.ArrowTip, _objectParent, _container);
            _tipsController = new TipsController(_arrowTip, _fig, _allEvents, PlayStartSound, _gameConfig);
        }
        private void SpawnFigure()
        {
            GameObject go = _levelDataCollection.CurrentLevel.levelData.Prefab.gameObject;
            _fig = _assetProviderService.Instantiate<Figure.Figure>(go, _objectParent, _container);
        }
        private void SpawnCharacter()
        {
            _character = _assetProviderService.Instantiate<Ufo>(Constants.CharacterUfo, _objectParent, _container);
        }

        private void SubscribeToEvents()
        {
            _gameSceneLoadedEvent = _allEvents[EventType.GameSceneLoaded];
            _gameSceneLoadedEvent.Event += InitFigure;
            _gameSceneLoadedEvent.Event += PlayStartSound;

            _levelCleared = _allEvents[EventType.LevelCleared];
            _levelCleared.Event += Win;
             
        }

        public void PlayStartSound()
        {
            AudioClip startClip = _levelDataCollection.CurrentLevel.levelData.TaskSound;
            _soundManager.Play(startClip);
        }

        private async void Win()
        {
            if (isWin) return;
            isWin = true;
            
            float waitToBootNewScene = _soundManager.PlayWin();
            await Task.Delay((int)(waitToBootNewScene * 1000));
            
            _levelDataCollection.GetNextLevel();
            _gameState.Enter<GameState>();
        }

        private void InitFigure()
        {
            _fig.InitFigure(_levelDataCollection.CurrentLevel.objectColor);
        }
    }
}