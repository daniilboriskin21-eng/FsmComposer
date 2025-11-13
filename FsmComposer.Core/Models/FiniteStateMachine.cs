public abstract class FiniteStateMachine
{
    public HashSet<string> States { get; internal set; } = new();
    public HashSet<char> Alphabet { get; internal set; } = new();
    public Dictionary<(string state, char? symbol), HashSet<string>> Transitions { get; } = new();
    public string StartState { get; internal set; } = "";
    public HashSet<string> AcceptStates { get; internal set; } = new();

    public void SetStates(IEnumerable<string> states) => States = new HashSet<string>(states);
    public void SetAlphabet(IEnumerable<char> alphabet) => Alphabet = new HashSet<char>(alphabet);
    public void SetStartState(string state) => StartState = state;
    public void SetAcceptStates(IEnumerable<string> acceptStates) => AcceptStates = new HashSet<string>(acceptStates);

    public abstract void AddTransition(string from, char? symbol, string to);
    public abstract HashSet<string> GetNextStates(string current, char? symbol);
    public abstract bool Accepts(string input);
}