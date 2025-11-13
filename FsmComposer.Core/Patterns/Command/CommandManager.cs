using System.Collections.Generic;

namespace FsmComposer.Core.Patterns.Command
{
    public class CommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();

        public void Execute(ICommand command)
        {
            command.Execute();
            if (command.CanUndo)
                _undoStack.Push(command);
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
                _undoStack.Pop().Undo();
        }
    }
}