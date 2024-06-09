using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Services;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.Models
{
    public class Particle : ReactiveObject
    {
        #region Properties

        private readonly ISimulationService _simulationService;

        [Reactive]
        public Vector2 Position { get; private set; }

        public Vector2 Velocity { get; set; }

        public float Theta { get; set; } = float.Pi;

        #endregion

        #region Constructors

        public Particle(ISimulationService simulationService, Vector2 coords)
        {
            _simulationService = simulationService;
            Position = coords;
            Velocity = new Vector2(-_simulationService.C, 0);
        }

        #endregion

        #region Methods

        public void Update()
        {
            Position += Velocity * _simulationService.DeltaTime;
        }

        #endregion
    }
}
