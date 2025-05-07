namespace Infrastructure.StateMachine.States
{
    public interface IPayloadState<in T> : IExitableState
    {
        void Enter(T param);
    }
}