using Infrastructure.Game;
using Systems.File;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BoostrapState : IPayloadState<int>
    {
        private readonly GameStateMachine _stateMachine;

        public BoostrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enter(int param)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            _stateMachine.Enter<MenuState>();
        }

        public void Exit() { }
    }
}