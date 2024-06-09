using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Black_Hole.MVVM.Models;
using Black_Hole.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.ViewModels
{
    public class OptionsViewModel : ReactiveObject
    {
        #region Singleton

        private static OptionsViewModel? _instance;

        public static OptionsViewModel Instance
        {
            get
            {
                _instance ??= new OptionsViewModel((SimulationService)App.ServiceProvider!.GetService(typeof(ISimulationService))!);
               
                return _instance;
            }
        }

        #endregion

        #region Properties

        private readonly ISimulationService _simulationService;

        public bool IsOpen { get; set; }

        [Reactive] 
        public float C { get; set; } = 30f;

        [Reactive]
        public float G { get; set; } = 3f;

        [Reactive] 
        public float DeltaTime { get; set; } = 0.3f;

        [Reactive] 
        public int BlackHoleMass { get; set; } = BlackHoleViewModel.Instance.BlackHole.Mass;


        #endregion

        #region Constructors

        private OptionsViewModel(ISimulationService simulationService)
        {
            _simulationService = simulationService;

            _simulationService.C
                .WhenAnyValue(x => x)
                .Subscribe(value =>
                {
                    C = value;
                });

            _simulationService.G
                .WhenAnyValue(x => x)
                .Subscribe(value =>
                {
                    G = value;
                });

            _simulationService.DeltaTime
                .WhenAnyValue(x => x)
                .Subscribe(value =>
                {
                    DeltaTime = value;
                });

            BlackHoleViewModel.Instance.BlackHole.Mass
                .WhenAnyValue(x => x)
                .Subscribe(value =>
                {
                    BlackHoleMass = value;
                });

            this.WhenAnyValue(x => x.C)
                .Subscribe(value =>
                {
                    _simulationService.C = value;
                });

            this.WhenAnyValue(x => x.G)
                .Subscribe(value =>
                {
                    _simulationService.G = value;
                });

            this.WhenAnyValue(x => x.DeltaTime)
                .Subscribe(value =>
                {
                    _simulationService.DeltaTime = value;
                });

            this.WhenAnyValue(x => x.BlackHoleMass)
                .Subscribe(value =>
                {
                    BlackHoleViewModel.Instance.BlackHole.Mass = value;
                });
        }

        #endregion
    }
}
