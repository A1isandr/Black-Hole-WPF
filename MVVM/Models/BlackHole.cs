using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.Helpers;

namespace Black_Hole.MVVM.Models
{
    public class BlackHole
    {
        #region Properties

        public Vector2 Position { get; set; } = new((float)Application.Current.MainWindow!.ActualWidth / 2f, (float)Application.Current.MainWindow.ActualHeight / 2f);

        public int Mass { get; set; } = 1000;

        #endregion

        #region Constructors

        public BlackHole()
        {
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
        }

        #endregion
    }
}
