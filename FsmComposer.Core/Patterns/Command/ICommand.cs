namespace FsmComposer.Core.Patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        bool CanUndo { get; }
    }
}