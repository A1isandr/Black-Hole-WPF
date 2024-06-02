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
    public class Particle(Vector2 coords) : ReactiveObject
    {
        #region Properties

        [Reactive]
        public Vector2 Position { get; private set; } = coords;

        public Vector2 Velocity { get; set; } = new(-Simulation.C, 0);

        private readonly TimeSpan _historyInterval = TimeSpan.FromMilliseconds(100);

        #endregion

        #region Methods

        public void Update()
        {
            Position += Velocity * Simulation.DeltaTime;
        }

        #endregion
    }
}
