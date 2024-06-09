using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.ViewModels
{
    public class DashboardViewModel : ReactiveObject
    {
        #region Properties

        [Reactive]
        public int NumParticles { get; private set; }

        [Reactive]
        public DashboardState DashboardState { get; private set; }

        [Reactive]
        public SimulationState SimulationState { get; private set; } = SimulationViewModel.Instance.SimulationState;

        [Reactive]
        public DashboardAnimationState DashboardAnimationState { get; set; } = DashboardAnimationState.Completed;

        private readonly IParticlesService _particlesService;

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> ResetSimulationCommand { get; }

        public ReactiveCommand<Unit, Unit> StopStartSimulationCommand { get; }

        public ReactiveCommand<Unit, Unit> LockUnlockDashboardCommand { get; }

        #endregion

        #region Constructors

        public DashboardViewModel(IParticlesService particlesService)
        {
            _particlesService = particlesService;

            var canLockUnlockDashboard = this
                .WhenAnyValue(x => x.DashboardAnimationState)
                .Select(state => state == DashboardAnimationState.Completed);

            StopStartSimulationCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                SimulationViewModel.Instance.StartStopSimulationCommand.Execute().Subscribe();
                SimulationState = SimulationViewModel.Instance.SimulationState;
            });

            ResetSimulationCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                SimulationViewModel.Instance.ResetSimulationCommand.Execute().Subscribe();
            });

            LockUnlockDashboardCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                DashboardState = DashboardState switch
                {
                    DashboardState.Locked => DashboardState.Unlocked,
                    DashboardState.Unlocked => DashboardState.Locked,
                    _ => DashboardState
                };
            }, canLockUnlockDashboard);

            _particlesService
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(e =>
                {
                    NumParticles += e.Adds;
                    NumParticles -= e.Removes;
                });
        }

        #endregion
    }

    public enum DashboardState
    {
        Locked,
        Unlocked
    }

    public enum DashboardAnimationState
    {
        InProgress,
        Completed
    }
}
