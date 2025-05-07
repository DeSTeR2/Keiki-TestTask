using System;
using System.Collections;
using Animations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure.StateMachine
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] AnimationController loadingAnimation;
        [SerializeField] AnimationController curtainDisappearAnimation;
        [SerializeField] CanvasGroup canvasGroup;

        [Space] 
        [SerializeField] private TextMeshProUGUI loadingText;
        [SerializeField] float timeToChangeText = 0.3f;
        bool _isRunning = false;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            loadingAnimation.StartAnimation();
        }

        public void StartLoading()
        {
            canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            _isRunning = true;
            StartCoroutine(TextAnimation());
        }

        public void StopLoading()
        {
            StopCoroutine(TextAnimation());
            curtainDisappearAnimation.StartAnimation().Callback(CurtainDisappear);
        }

        private void CurtainDisappear()
        {
            _isRunning = false;
            gameObject.SetActive(false);
        }
        
        private IEnumerator TextAnimation()
        {
            int dots = 0;
            while (_isRunning)
            {
                loadingText.text = "Loading";
                for (int i = 0; i < dots; i++)
                {
                    loadingText.text += ".";
                }

                dots++;
                dots %= 4;

                yield return new WaitForSeconds(timeToChangeText);
            }
        }
    }
}