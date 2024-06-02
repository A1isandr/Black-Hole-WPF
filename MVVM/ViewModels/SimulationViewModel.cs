using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using Black_Hole.Services;
using DynamicData.Alias;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.ViewModels
{
	public class SimulationViewModel : ReactiveObject
	{
        #region Singleton

        private static SimulationViewModel? _instance;

        public static SimulationViewModel Instance
        {
            get
            {
                _instance ??= new SimulationViewModel
                    (
                        (ParticlesService)App.ServiceProvider!.GetService(typeof(IParticlesService))!,
                        (ParticlesHistoryService)App.ServiceProvider!.GetService(typeof(IParticlesHistoryService))!
                    );

                return _instance;
            }
        }

        #endregion

        #region Properties

        private readonly Simulation _simulation;

        private readonly IParticlesService _particlesService;

        private readonly IParticlesHistoryService _particlesHistoryService;

        [Reactive]
        public SimulationState SimulationState { get; private set; }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> StartStopSimulationCommand { get; }

        public ReactiveCommand<Point, Unit> SpawnParticleCommand { get; }

        public ReactiveCommand<Unit, Unit> ResetSimulationCommand { get; }

        #endregion

        #region Constructors

        private SimulationViewModel(IParticlesService particlesService, IParticlesHistoryService particlesHistoryService)
        {
            _particlesService = particlesService;
            _particlesHistoryService = particlesHistoryService;

            _simulation = new Simulation(_particlesService);

            StartStopSimulationCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                if (_simulation.IsRunning)
                {
                    _simulation.Stop();
                    _particlesHistoryService.Stop();
                    SimulationState = SimulationState.Stopped;
                }
                else
                {
                    SimulationState = SimulationState.Running;

                    RxApp.TaskpoolScheduler.Schedule(_ =>
                    {
                        _simulation.Run();
                    });

                    _particlesHistoryService.Start();
                }
            });

            SpawnParticleCommand = ReactiveCommand.Create<Point>(point =>
            {
                _particlesService.SpawnParticle(point);
            });

            ResetSimulationCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                _particlesService.ClearParticles();
                _particlesHistoryService.ClearHistory();
            });
        }

        #endregion
    }

    public enum SimulationState
    {
        Running,
        Stopped
    }
}
