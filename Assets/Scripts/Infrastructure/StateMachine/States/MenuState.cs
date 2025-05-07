using System.Transactions;
using Infrastructure.Game;
using Infrastructure.Services;

namespace Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly GameFactory _gameFactory;

        public MenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(Scenes.MenuScene);
        }

        public void Exit() {}
    }
}