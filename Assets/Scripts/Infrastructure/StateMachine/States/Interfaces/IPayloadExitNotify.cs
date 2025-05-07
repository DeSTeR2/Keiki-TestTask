using System;

namespace Infrastructure.StateMachine.States
{
    public interface IPayloadExitNotify<in T> : IExitableState
    {
        void Enter(T param, Action onExit = null);
    }
}