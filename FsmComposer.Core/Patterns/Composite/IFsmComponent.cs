using System.Collections.Generic;

namespace FsmComposer.Core.Patterns.Composite
{
    public interface IFsmComponent
    {
        void Add(IFsmComponent component);
        void Remove(IFsmComponent component);
        IEnumerable<IFsmComponent> GetChildren();
        string Simulate(string input);
        string Name { get; }
    }
}