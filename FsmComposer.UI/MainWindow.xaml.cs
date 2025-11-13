using FsmComposer.Core.Models;
using FsmComposer.Core.Patterns.AbstractFactory;
using FsmComposer.Core.Patterns.Command;
using FsmComposer.Core.Patterns.Composite;
using FsmComposer.UI.Services;
using System.Windows;
using System.Windows.Controls;

namespace FsmComposer.UI
{
    public partial class MainWindow : Window
    {
        private CompositeFsm? _rootFsm;
        private Simulator? _simulator;
        private readonly CommandManager _commandManager = new();

        public MainWindow()
        {
            InitializeComponent();
            _rootFsm = new CompositeFsm("Root");
        }

        private void CreateFsm_Click(object sender, RoutedEventArgs e)
        {
            IFiniteStateMachineFactory factory = FsmTypeCombo.SelectedIndex == 0
                ? new DfaFactory()
                : new NfaFactory();

            var fsm = factory.CreateFsm();
            ConfigureFsm(fsm);

            var atomic = new AtomicFsm(fsm, $"FSM_{System.DateTime.Now:HHmmss}");
            _rootFsm.Add(atomic);

            _simulator = new Simulator(fsm);
            _simulator.LoadInput(InputText.Text);

            DrawFsm();
            StatusText.Text = $"Создан {fsm.GetType().Name}";
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            if (_simulator != null)
            {
                var command = new StepCommand(_simulator);
                _commandManager.Execute(command);
                StatusText.Text = $"Состояние: {_simulator.CurrentState}";
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            _commandManager.Undo();
            StatusText.Text = $"Отменено. Состояние: {_simulator?.CurrentState}";
        }

        private void ConfigureFsm(FiniteStateMachine fsm)
        {
            fsm.SetStates(new[] { "q0", "q1" });
            fsm.SetAlphabet(new[] { '0', '1' });
            fsm.SetStartState("q0");
            fsm.SetAcceptStates(new[] { "q1" });

            fsm.AddTransition("q0", '0', "q0");
            fsm.AddTransition("q0", '1', "q1");
            fsm.AddTransition("q1", '0', "q1");
            fsm.AddTransition("q1", '1', "q0");
        }
        private void DrawFsm()
        {
            if (_simulator?.Fsm is FiniteStateMachine fsm)
            {
                FsmVisualizer.DrawFsm(FsmCanvas, fsm);
            }
        }
    }
}