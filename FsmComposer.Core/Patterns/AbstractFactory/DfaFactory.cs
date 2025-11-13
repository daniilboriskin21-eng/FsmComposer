using FsmComposer.Core.Models;

namespace FsmComposer.Core.Patterns.AbstractFactory
{
    public class DfaFactory : IFiniteStateMachineFactory
    {
        public FiniteStateMachine CreateFsm() => new DeterministicFsm();
    }
}