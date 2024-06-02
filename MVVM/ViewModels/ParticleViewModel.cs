using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.ViewModels
{
	public class ParticleViewModel : ReactiveObject
    {
        #region Properties

        public Particle Particle { get; init; }

        [Reactive]
        public float X { get; set; }

        [Reactive] 
        public float Y { get; set; }

        #endregion

        #region Constructors

        public ParticleViewModel(Particle particle)
        {
            Particle = particle;

            this.WhenAnyValue(x => x.Particle.Position.X, x => x.Particle.Position.Y)
                .Subscribe(newCoords =>
                {
                    X = newCoords.Item1;
                    Y = newCoords.Item2;
                });
        }

        #endregion
    }
}
