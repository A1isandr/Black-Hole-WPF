using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.MVVM.Models
{
    public class Particle(Vector2 coords) : ReactiveObject
    {
        #region Properties

        [Reactive]
        public Vector2 Position { get; set; } = coords;

        public Vector2 Velocity { get; } = new(-Simulation.C, 0);

        #endregion

    }
}
