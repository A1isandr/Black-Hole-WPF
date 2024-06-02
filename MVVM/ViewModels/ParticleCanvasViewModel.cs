using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABI.Windows.Foundation;
using Black_Hole.MVVM.Models;
using Black_Hole.Services;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Point = System.Windows.Point;

namespace Black_Hole.MVVM.ViewModels
{
    public class ParticleCanvasViewModel : ReactiveObject
    {
        #region Properties

        private readonly ReadOnlyObservableCollection<ParticleViewModel> _particles;
        public ReadOnlyObservableCollection<ParticleViewModel> Particles => _particles;

        private readonly IParticlesService _particlesService;

        [Reactive]
        public bool ShowTip { get; private set; }

        #endregion

        #region Commands

        

        #endregion

        #region Constructors

        public ParticleCanvasViewModel(IParticlesService particlesService)
        {
            _particlesService = particlesService;

            _particlesService
                .Connect()
                .Transform(particle => new ParticleViewModel(particle))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _particles)
                .Subscribe();

            var particlesEmpty = this
                .WhenAnyValue(x => x._particles.Count)
                .Select(count => count == 0);

            particlesEmpty
                .Throttle(TimeSpan.FromMilliseconds(5000))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(isEmpty =>
                {
                    if (isEmpty) ShowTip = true;
                });

            particlesEmpty
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(isEmpty =>
                {
                    if (!isEmpty) ShowTip = false;
                });
        }

        #endregion
    }
}
