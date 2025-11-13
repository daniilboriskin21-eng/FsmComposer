using System.Collections.Generic;
using System.Linq;

namespace FsmComposer.Core.Patterns.Composite
{
    public class CompositeFsm : IFsmComponent
    {
        private readonly List<IFsmComponent> _children = new();
        public string Name { get; }

        public CompositeFsm(string name) => Name = name;

        public void Add(IFsmComponent component) => _children.Add(component);
        public void Remove(IFsmComponent component) => _children.Remove(component);
        public IEnumerable<IFsmComponent> GetChildren() => _children;

        public string Simulate(string input)
        {
            return string.Join(" → ", _children.Select(c => $"{c.Name}: {c.Simulate(input)}"));
        }
    }
}