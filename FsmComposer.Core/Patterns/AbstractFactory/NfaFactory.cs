using FsmComposer.Core.Models;

namespace FsmComposer.Core.Patterns.AbstractFactory
{
    public class NfaFactory : IFiniteStateMachineFactory
    {
        public FiniteStateMachine CreateFsm() => new NondeterministicFsm();
    }
}