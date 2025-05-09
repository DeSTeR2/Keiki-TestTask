using System;
using System.Collections;
using Infrastructure.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly float _forcedTimeToWait;

        public SceneLoader(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, float forcedTimeToWait = 0)
        {
            _coroutineRunner = coroutineRunner;
            _loadingCurtain = loadingCurtain;
            _forcedTimeToWait = forcedTimeToWait;
        }

        public void LoadWithForcedWaiting(string sceneName, Action onLoad = null) => 
            _coroutineRunner?.StartCoroutine(LoadScene(sceneName, onLoad, _forcedTimeToWait));
        
        public void LoadWithTrueWaitTime(string sceneName, Action onLoad = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoad));
        
        private IEnumerator LoadScene(string sceneName, Action onLoad, float forcedWaitTime = 0)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            _loadingCurtain.StartLoading();
            
            while (!asyncOperation.isDone)
                yield return null;

            yield return new WaitForSeconds(forcedWaitTime);
            
            _loadingCurtain.StopLoading();
            onLoad?.Invoke();
        }
    }
}