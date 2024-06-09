using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.Helpers;
using Black_Hole.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.Models
{
    public class BlackHole : ReactiveObject
    {
        #region Properties

        private readonly ISimulationService _simulationService;

        private readonly IParticlesService _particlesService;

        private Vector2 Position { get; set; } = new((float)Application.Current.MainWindow!.ActualWidth / 2f, (float)Application.Current.MainWindow.ActualHeight / 2f);

        [Reactive]
        public int Mass { get; set; } = 1000;

        public float Rs { get; set; }

        #endregion

        #region Constructors

        public BlackHole(ISimulationService simulationService, IParticlesService particlesService)
        {
            _simulationService = simulationService;

            _particlesService = particlesService;

            Rs = (2 * _simulationService.G * Mass) / (_simulationService.C * _simulationService.C);

            Application.Current.MainWindow!.SizeChanged += (_, _) =>
            {
                Position = new Vector2((float)Application.Current.MainWindow!.ActualWidth / 2f, (float)Application.Current.MainWindow.ActualHeight / 2f);
            };
        }

        #endregion

        #region Methods

        public void Pull(Particle particle)
        {
            var force = Position - particle.Position;
            var theta = force.Heading();
            var r = force.Magnitude();
            var gravityForce = _simulationService.G * Mass / (r * r);
            var deltaTheta = -gravityForce * (_simulationService.DeltaTime / _simulationService.C) * MathF.Sin(particle.Theta - theta);
            deltaTheta /= MathF.Abs(1.0f - (2.0f * _simulationService.G * Mass) / (r * _simulationService.C * _simulationService.C));
            particle.Theta += deltaTheta;
            particle.Velocity = Vector2Extensions.FromAngle(particle.Theta);

            if (r < Rs)
            {
                _particlesService.AddToDeleteCandidates(particle);
            }
        }

        #endregion
    }
}
