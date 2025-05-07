using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class AnimationSequence : MonoBehaviour
    {
        [SerializeField] List<Animations> controllers;

        private Action Callback;

        public async void StartAnimation()
        {
            bool isThereInfLoop = false;
            int animationEnded = 0;

            SetObjectsToStartValues();
            for (int i = 0; i < controllers.Count; i++)
            {
                Animations anim = controllers[i];
                float animationDelay = anim.delay;

                if (anim.animation.IsThereInfiniteLoop())
                    isThereInfLoop = true;

                await Task.Delay((int)(animationDelay * 1000f));
                anim.animation.StartAnimation().Callback(() => animationEnded++);
            }

            if (isThereInfLoop) return;

            while (animationEnded < controllers.Count)
                await Task.Yield();

            InvokeCallback();
        }


        public async void StartReverceAnimation()
        {
            bool isThereInfLoop = false;
            int animationEnded = 0;

            for (int i = controllers.Count - 1; i >= 0 ; i--)
            {
                Animations anim = controllers[i];
                float animationDelay = anim.delay;

                if (anim.animation.IsThereInfiniteLoop())
                    isThereInfLoop = true;

                await Task.Delay((int)(animationDelay * 1000f));
                anim.animation.StartReverceAnimation().Callback(() => animationEnded++);
            }

            if (isThereInfLoop) return;

            while (animationEnded < controllers.Count)
                await Task.Yield();

            InvokeCallback();
        }


        private void InvokeCallback()
        {
            Callback?.Invoke();
            Callback = null;
        }
        private void SetObjectsToStartValues()
        {
            foreach (Animations controller in controllers)
            {
                controller.animation.SetToStartValue();
            }
        }
    }

    [Serializable]
    public struct Animations
    {
        public AnimationController animation;
        public float delay;
    }
}