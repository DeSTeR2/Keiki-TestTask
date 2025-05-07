using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.StateMachine.States;
using Systems.File;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : StateMachine
    {
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        
        public Action OnSceneChanged;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            RegisterStates();
        }

        protected override void RegisterStates()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BoostrapState)] = new BoostrapState(this),
                [typeof(LoadingState)] = new LoadingState(_sceneLoader),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(GameState)] = new GameState(this)
            };
        }
    }
}