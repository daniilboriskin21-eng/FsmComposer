using System.Collections.Generic;
using System.Linq;
using FsmComposer.Core.Models;

namespace FsmComposer.Core.Patterns.Composite
{
    public class AtomicFsm : IFsmComponent
    {
        public FiniteStateMachine Fsm { get; }
        public string Name { get; }

        public AtomicFsm(FiniteStateMachine fsm, string name)
        {
            Fsm = fsm; Name = name;
        }

        public void Add(IFsmComponent component) => throw new System.NotSupportedException();
        public void Remove(IFsmComponent component) => throw new System.NotSupportedException();
        public IEnumerable<IFsmComponent> GetChildren() => Enumerable.Empty<IFsmComponent>();

        public string Simulate(string input)
        {
            var sim = new Simulator(Fsm);
            sim.LoadInput(input);
            while (sim.HasNext) sim.Step();
            return sim.IsAccepted ? "Accepted" : "Rejected";
        }
    }
}