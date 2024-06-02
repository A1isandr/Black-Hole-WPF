using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Services;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using RegExp2DFA.Helpers;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
    {
        private readonly Storyboard _flyOutAnimation;
        private readonly Storyboard _flyInAnimation;

        public DashboardView()
        {
            InitializeComponent();

            ViewModel = new DashboardViewModel
            (
                //BUG: Из за этого появляется ошибка в предпросмотре.
                (ParticlesService)App.ServiceProvider!.GetService(typeof(IParticlesService))!
            );

            _flyOutAnimation = (Storyboard)TryFindResource("FlyOutAnimation");
            _flyInAnimation = (Storyboard)TryFindResource("FlyInAnimation");

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                    viewModel => viewModel.NumParticles,
                    view => view.NumParticlesTextBlock.Text)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.ResetSimulationCommand,
                        view => view.ResetSimulationButton)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.StopStartSimulationCommand,
                        view => view.StopStartSimulationButton)
                    .DisposeWith(disposables);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.LockUnlockDashboardCommand,
                        view => view.LockUnlockDashboardButton)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.SimulationState,
                        view => view.StopStartSimulationButton.Content,
                        SimulationStateToStartStopButtonIcon)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.DashboardState,
                        view => view.LockUnlockDashboardButton.Content,
                        DashboardStateToLockUnlockDashboardButtonIcon)
                    .DisposeWith(disposables);

                this
                    .Events()
                    .MouseEnter
                    .Subscribe(e =>
                    {
                        if (ViewModel.DashboardState == DashboardState.Unlocked &&
                            ViewModel.DashboardAnimationState == DashboardAnimationState.Completed)
                        {
                            ViewModel.DashboardAnimationState = DashboardAnimationState.InProgress;
                            _flyOutAnimation.Begin();
                        }
                    })
                    .DisposeWith(disposables);

                this
                    .Events()
                    .MouseLeave
                    .Subscribe(e =>
                    {
                        if (ViewModel.DashboardState == DashboardState.Unlocked &&
                            ViewModel.DashboardAnimationState == DashboardAnimationState.Completed)
                        {
                            ViewModel.DashboardAnimationState = DashboardAnimationState.InProgress;
                            _flyInAnimation.Begin();
                        }
                    })
                    .DisposeWith(disposables);

                _flyOutAnimation
                    .Events()
                    .Completed
                    .Subscribe(e =>
                    {
                        if (ViewModel != null) ViewModel.DashboardAnimationState = DashboardAnimationState.Completed;
                    });

                _flyInAnimation
                    .Events()
                    .Completed
                    .Subscribe(e =>
                    {
                        if (ViewModel != null) ViewModel.DashboardAnimationState = DashboardAnimationState.Completed;
                    });
            });

        }

        private object? SimulationStateToStartStopButtonIcon(SimulationState state)
        {
            var resource = state switch
            {
                SimulationState.Running => Application.Current.FindResource(IconResourcesKeys.PauseIconKey),
                SimulationState.Stopped => Application.Current.FindResource(IconResourcesKeys.PlayIconKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert SimulationState to StopStartButtonIcon."),
            };

            return ((GeometryDrawing)resource!).Geometry;
        }

        private object? DashboardStateToLockUnlockDashboardButtonIcon(DashboardState state)
        {
            var resource = state switch
            {
                DashboardState.Unlocked => Application.Current.FindResource(IconResourcesKeys.LockOpenIconKey),
                DashboardState.Locked => Application.Current.FindResource(IconResourcesKeys.LockIconKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert DashboardState to LockUnlockDashboardButtonIcon."),
            };

            return ((GeometryDrawing)resource!).Geometry;
        }
    }
}
