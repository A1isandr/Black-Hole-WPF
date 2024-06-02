using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
    public class ParticleHistoryViewModel(Vector2 position) : ReactiveObject
    {
        #region Properties

        public Vector2 Position { get; init; } = position;

        public float X => Position.X;

        public float Y => Position.Y;

        #endregion
    }
}
