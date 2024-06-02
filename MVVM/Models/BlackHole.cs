using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.Helpers;
using Black_Hole.Services;

namespace Black_Hole.MVVM.Models
{
    public class BlackHole
    {
        #region Properties

        private readonly IParticlesService _particlesService;

        public Vector2 Position { get; set; } = new((float)Application.Current.MainWindow!.ActualWidth / 2f, (float)Application.Current.MainWindow.ActualHeight / 2f);

        public int Mass { get; set; } = 1000;

        public float Rs { get; set; }

        #endregion

        #region Constructors

        public BlackHole(IParticlesService particlesService)
        {
            _particlesService = particlesService;

            Rs = (2 * Simulation.G * Mass) / (Simulation.C * Simulation.C);

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
            var r = force.Magnitude();
            var gravityForce = Simulation.G * Mass / (r * r);
            force = force.SetMagnitude(gravityForce);
            particle.Velocity += force;
            particle.Velocity = particle.Velocity.SetMagnitude(Simulation.C);

            if (r < Rs)
            {
                _particlesService.AddToDeleteCandidates(particle);
            }
        }

        #endregion
    }
}
