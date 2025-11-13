using System.Collections.Generic;
using System.Linq;

namespace FsmComposer.Core.Models
{
    public class Simulator
    {
        private readonly FiniteStateMachine _fsm;
        private HashSet<string> _current = new();
        private readonly Queue<char> _input = new();

        public string CurrentState => string.Join(", ", _current);
        public bool HasNext => _input.Count > 0;
        public char NextSymbol => _input.Peek();
        public bool IsAccepted => _current.Overlaps(_fsm.AcceptStates);
        public FiniteStateMachine Fsm => _fsm;

        public Simulator(FiniteStateMachine fsm)
        {
            _fsm = fsm;
            Reset();
        }

        public void LoadInput(string input)
        {
            _input.Clear();
            foreach (char c in input) _input.Enqueue(c);
        }

        public void Reset() => _current = new HashSet<string> { _fsm.StartState };

        public void Step()
        {
            if (!HasNext) return;
            char symbol = _input.Dequeue();
            var next = new HashSet<string>();
            foreach (var state in _current)
                next.UnionWith(_fsm.GetNextStates(state, symbol));
            _current = next;
        }

        public void UndoStep(string previousState)
        {
            _current = previousState.Split(", ").ToHashSet();
        }
    }
}