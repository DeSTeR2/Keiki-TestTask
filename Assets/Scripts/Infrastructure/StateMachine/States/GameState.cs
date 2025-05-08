using System;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameState : IState
    {
        private const string GameScene = "GameScene";
        private readonly GameStateMachine _gameStateMachine;

        public GameState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(GameScene, OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public void Exit() { }
    }
}