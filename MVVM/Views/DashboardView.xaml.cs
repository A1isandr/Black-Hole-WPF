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
using Black_Hole.Resources.Keys;
using Black_Hole.Services;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
    {
        private readonly Storyboard _flyOutAnimation;
        private readonly Storyboard _flyInAnimation;

        private readonly Storyboard _resetButtonAnimation;
        private readonly Storyboard _settingsButtonAnimation;

        public DashboardView()
        {
            InitializeComponent();

            ViewModel = new DashboardViewModel
            (
                //BUG: Из-за этого появляется ошибка в предпросмотре?
                (ParticlesService)App.ServiceProvider!.GetService(typeof(IParticlesService))!
            );

            _flyOutAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.DashboardFlyOutAnimationKey);
            _flyInAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.DashboardFlyInAnimationKey);

            _resetButtonAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.DashboardResetButtonAnimationKey);
            _settingsButtonAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.DashboardSettingsButtonAnimationKey);

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

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.SimulationState,
                        view => view.StopStartSimulationButtonToolTip.Text,
                        SimulationStateToStartStopButtonToolTipText)
                    .DisposeWith(disposables);

                this.OneWayBind(ViewModel,
                        viewModel => viewModel.DashboardState,
                        view => view.LockUnlockDashboardButtonToolTip.Text,
                        DashboardStateToLockUnlockButtonToolTipText)
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

                ResetSimulationButton
                    .Events()
                    .Click
                    .Subscribe(e =>
                    {
                        _resetButtonAnimation.Begin();
                    })
                    .DisposeWith(disposables);

                SettingsButton
                    .Events()
                    .Click
                    .Subscribe(e =>
                    {
                        _settingsButtonAnimation.Begin();
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
                SimulationState.Running => Application.Current.FindResource(SvgImagesKeys.PauseIconKey),
                SimulationState.Stopped => Application.Current.FindResource(SvgImagesKeys.PlayIconKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert SimulationState to StopStartButtonIcon."),
            };

            return ((GeometryDrawing)resource!).Geometry;
        }

        private string? SimulationStateToStartStopButtonToolTipText(SimulationState state)
        {
            var resource = state switch
            {
                SimulationState.Running => Application.Current.FindResource(TextResourcesKeys.DashboardStopStartButtonRunningToolTipTextKey),
                SimulationState.Stopped => Application.Current.FindResource(TextResourcesKeys.DashboardStopStartButtonStoppedToolTipTextKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert SimulationState to StopStartButtonToolTipText."),
            };

            return resource?.ToString();
        }

        private object? DashboardStateToLockUnlockDashboardButtonIcon(DashboardState state)
        {
            var resource = state switch
            {
                DashboardState.Unlocked => Application.Current.FindResource(SvgImagesKeys.LockOpenIconKey),
                DashboardState.Locked => Application.Current.FindResource(SvgImagesKeys.LockIconKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert DashboardState to LockUnlockDashboardButtonIcon."),
            };

            return ((GeometryDrawing)resource!).Geometry;
        }

        private string? DashboardStateToLockUnlockButtonToolTipText(DashboardState state)
        {
            var resource = state switch
            {
                DashboardState.Unlocked => Application.Current.FindResource(TextResourcesKeys.DashboardLockUnlockButtonUnlockedToolTipTextKey),
                DashboardState.Locked => Application.Current.FindResource(TextResourcesKeys.DashboardLockUnlockButtonLockedToolTipTextKey),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Failed to convert DashboardState to LockUnlockButtonToolTipText."),
            };

            return resource?.ToString();
        }
    }
}
