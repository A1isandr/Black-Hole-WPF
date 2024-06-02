using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using Black_Hole.Services;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
    public class ParticlesHistoryCanvasViewModel : ReactiveObject
    {
        #region Properties

        private IParticlesHistoryService _particlesHistoryService;

        private readonly ReadOnlyObservableCollection<ParticleHistoryViewModel> _particlesHistory;
        public ReadOnlyObservableCollection<ParticleHistoryViewModel> ParticlesHistory => _particlesHistory;

        #endregion

        #region Constructors

        public ParticlesHistoryCanvasViewModel(IParticlesHistoryService particlesHistoryService)
        {
            _particlesHistoryService = particlesHistoryService;

            _particlesHistoryService
                .Connect()
                .Transform(position => new ParticleHistoryViewModel(new Vector2(position.X + 7.5f, position.Y + 7.5f)))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _particlesHistory)
                .Subscribe();
        }

        #endregion
    }
}
