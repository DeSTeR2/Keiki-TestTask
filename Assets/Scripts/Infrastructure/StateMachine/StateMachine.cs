using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public abstract class StateMachine
    {
        protected Dictionary<Type, IExitableState> _states;
        private IExitableState _currectState;
        protected abstract void RegisterStates();
        
        public void Enter<TState>() where TState : IState
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TState)];
            (_currectState as IState)?.Enter();
        }

        public void Enter<TStateWithParam, TParam>(TParam param) where TStateWithParam : class, IPayloadState<TParam>
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TStateWithParam)];
            (_currectState as TStateWithParam)?.Enter(param);
        }

        public void Enter<TStateWithParamExitNotify, TParam>(TParam param, Action onExit = null) where TStateWithParamExitNotify : class, IPayloadExitNotify<TParam>
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TStateWithParamExitNotify)];
            (_currectState as TStateWithParamExitNotify)?.Enter(param, onExit);
        }
    }
}