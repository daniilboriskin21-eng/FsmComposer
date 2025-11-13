namespace FsmComposer.Core.Models
{
    public class DeterministicFsm : FiniteStateMachine
    {
        public override void AddTransition(string from, char? symbol, string to)
        {
            Transitions[(from, symbol)] = new HashSet<string> { to };
        }

        public override HashSet<string> GetNextStates(string current, char? symbol)
        {
            Transitions.TryGetValue((current, symbol), out var next);
            return next ?? new HashSet<string>();
        }

        public override bool Accepts(string input)
        {
            var sim = new Simulator(this);
            sim.LoadInput(input);
            while (sim.HasNext) sim.Step();
            return sim.IsAccepted;
        }
    }
}