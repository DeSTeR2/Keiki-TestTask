using System;
using Infrastructure.Events;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameState : IState
    {
        private const string GameScene = "GameScene";
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventHolder _gameEvent;

        public GameState(GameStateMachine gameStateMachine, EventHolder gameEvent)
        {
            _gameStateMachine = gameStateMachine;
            _gameEvent = gameEvent;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(GameScene, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameEvent?.Invoke();
        }

        public void Exit() { }
    }
}