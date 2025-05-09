using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Events;
using Systems;
using UnityEngine;
using EventType = Infrastructure.Events.EventType;

namespace Game.Tips
{
    public class TipsController : IDisposable
    {
        private readonly ArrowTip _arrowTip;
        private readonly Figure.Figure _figure;
        private readonly Action _playStartSound;
        private readonly GameConfig _gameConfig;
        
        private CameraRayCastSystem _rayCastSystem;
        
        private float _inactiveTime = 0f;

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        private List<Tip> _tips;
        private AllEvents _allEvents;

        public TipsController(ArrowTip arrowTip, Figure.Figure figure, AllEvents allEvents, Action playStartSound, GameConfig gameConfig)
        {
            _allEvents = allEvents;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;   
            
            _arrowTip = arrowTip;
            _figure = figure;
            _playStartSound = playStartSound;
            _gameConfig = gameConfig;

            CreateTips();
            StartTimer();
            
            _allEvents[EventType.DraggingApproved].Event += ResetTimer;
        }

        private void CreateTips()
        {
            _tips = new List<Tip>();

            Tip soundTip = new Tip(_gameConfig.playStartSoundAfter, _gameConfig.timerStep, _playStartSound);
            Tip arrowTip = new Tip(_gameConfig.showArrowTipAfter, _gameConfig.timerStep, () =>
            {
                List<Vector3> points = _figure.GetCurrentPositions();
                _arrowTip.Move(points);
            });
             
            _tips.Add(soundTip);
            _tips.Add(arrowTip);
        }

        private async void StartTimer()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                await Task.Delay((int)(_gameConfig.timerStep * 1000));
                _inactiveTime += _gameConfig.timerStep;

                StartTips();
            }
        }

        private void StartTips()
        {
            foreach (Tip tip in _tips)
            {
                tip.StartTip(_inactiveTime);
            }
        }

        private void ResetTimer()
        {
            _inactiveTime = 0;
            _arrowTip.Stop();
        }

        public void Dispose()
        {
            if (_cancellationToken.CanBeCanceled)
            {
                _cancellationTokenSource.Cancel();
            }
            _cancellationTokenSource.Dispose();
            
            _allEvents[EventType.DraggingApproved].Event -= ResetTimer;
        }
    }
}