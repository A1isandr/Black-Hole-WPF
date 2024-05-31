using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Black_Hole.MVVM.Models;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
	public class MainViewModel : ReactiveObject
	{
        #region Singleton

        private static MainViewModel? _instance;

        public static MainViewModel Instance
        {
            get
            {
                _instance ??= new MainViewModel();
                return _instance;
            }
        }

        #endregion

        #region Properties

        private readonly ParticleCanvasViewModel _particleCanvasViewModel = ParticleCanvasViewModel.Instance;
        private readonly BlackHoleViewModel _blackHoleViewModel = BlackHoleViewModel.Instance; 

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> ResetSimulation { get; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            ResetSimulation = ReactiveCommand.Create<Unit>(_ =>
            {
                _particleCanvasViewModel.ClearParticlesCommand.Execute().Subscribe();
            });
        }

        #endregion
    }
}
