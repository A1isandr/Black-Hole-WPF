using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using Black_Hole.Services;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
	public class BlackHoleViewModel : ReactiveObject
	{
        #region Singleton

        private static BlackHoleViewModel? _instance;

        public static BlackHoleViewModel Instance
        {
            get
            {
                _instance ??= new BlackHoleViewModel(new BlackHole((ParticlesService)App.ServiceProvider!.GetService(typeof(IParticlesService))!));
                return _instance;
            }
        }

        #endregion

        #region Properties

        public BlackHole BlackHole { get; init; }

        public double AccretionDiskRadius { get; set; }

        public double ParticleOrbit { get; set; }

        public double Start { get; set; } = Application.Current.MainWindow!.Height / 2;

        public double End { get; set; }

        #endregion

        #region Constructors

        private BlackHoleViewModel(BlackHole blackHole)
        {
            BlackHole = blackHole;

            End = Application.Current.MainWindow!.Height / 2 - BlackHole.Rs * 2.6;

            this
                .WhenAnyValue(x => x.BlackHole.Rs)
                .Subscribe(rs => AccretionDiskRadius = rs * 3 + 128);

            this
                .WhenAnyValue(x => x.BlackHole.Rs)
                .Subscribe(rs => ParticleOrbit = rs * 1.5 + 64);
        }

        #endregion
    }
}
