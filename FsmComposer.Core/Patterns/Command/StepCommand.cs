using FsmComposer.Core.Models;

namespace FsmComposer.Core.Patterns.Command
{
    public class StepCommand : ICommand
    {
        private readonly Simulator _simulator;
        private string? _prevState;
        private char? _symbol;

        public bool CanUndo => _symbol.HasValue;

        public StepCommand(Simulator simulator) => _simulator = simulator;

        public void Execute()
        {
            if (_simulator.HasNext)
            {
                _prevState = _simulator.CurrentState;
                _symbol = _simulator.NextSymbol;
                _simulator.Step();
            }
        }

        public void Undo()
        {
            if (CanUndo && _prevState != null)
            {
                _simulator.UndoStep(_prevState);
                _symbol = null;
            }
        }
    }
}