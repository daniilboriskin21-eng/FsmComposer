using FsmComposer.Core.Models;

namespace FsmComposer.Core.Patterns.AbstractFactory
{
    public interface IFiniteStateMachineFactory
    {
        FiniteStateMachine CreateFsm();
    }
}