using System;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Game
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Button menuBtn;
        private GameStateMachine _stateMachine;

        [Inject]
        public void Construct(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            menuBtn.onClick.AddListener(BootMenuScene);
        }

        private void BootMenuScene()
        {
            _stateMachine.Enter<MenuState>();
        }
    }
}