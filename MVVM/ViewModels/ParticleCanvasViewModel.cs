using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.ViewModels
{
    public class ParticleCanvasViewModel : ReactiveObject
    {
        #region Singleton

        private static ParticleCanvasViewModel? _instance;

        public static ParticleCanvasViewModel Instance
        {
            get
            {
                _instance ??= new ParticleCanvasViewModel();
                return _instance;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<ParticleViewModel> Particles { get; set; } = new();

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> ClearParticlesCommand { get; }

        #endregion

        #region Constructors

        private ParticleCanvasViewModel()
        {
            ClearParticlesCommand = ReactiveCommand.Create<Unit>(_ =>
            {
                Particles.Clear();
            });
        }

        #endregion
    }
}
