using System;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class LoadingState : IPayloadExitNotify<string>
    {
        private readonly SceneLoader _sceneLoader;
        
        public LoadingState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(string param, Action onExit = null)
        {
            _sceneLoader.LoadWithForcedWaiting(param, onExit);
        }

        public void Exit()
        {
            Debug.Log("Exit LoadingState");
        }
    }
}