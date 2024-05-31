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

        public Vector2 Position { get; set; } = new((float)Application.Current.MainWindow!.ActualWidth, (float)Application.Current.MainWindow.ActualHeight);

        public int Mass { get; set; } = 3000;

        #endregion

        #region Constructors

        public BlackHole()
        {
            Application.Current.MainWindow!.SizeChanged += (_, _) =>
            {
                Position = new Vector2((float)Application.Current.MainWindow!.ActualWidth, (float)Application.Current.MainWindow.ActualHeight);
            };
        }

        #endregion

        #region Methods

        public Vector2 Pull(Particle particle)
        {
            var force = Position - particle.Position;
            var r = force.Magnitude();
            var fg = Simulation.G * Mass / (r * r);
            return Vector2.One;
        }

        #endregion
    }
}
