using System;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameState : IPayloadState<string>
    {
        private const string GameScene = "GameScene";
        private readonly GameStateMachine _gameStateMachine;

        public GameState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(string param)
        {
            _gameStateMachine.Enter<LoadingState, string>(GameScene, OnLoaded);
        }

        private void OnLoaded()
        {
            
        }

        public void Exit()
        {
        }
    }
}