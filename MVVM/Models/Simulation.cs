using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Services;

namespace Black_Hole.MVVM.Models
{
    public class Simulation
    {
        #region Constants

        public const int C = 50;
        public const float G = 2f;
        public const float DeltaTime = 0.01f;

        #endregion

        #region Properties

        public bool IsRunning { get; private set; }

        private readonly IParticlesService _particlesService;

        #endregion

        #region Constructors

        public Simulation(IParticlesService particlesService)
        {
            _particlesService = particlesService;
        }

        #endregion

        #region Methods

        public void Run()
        {
            IsRunning = true;

            while (IsRunning)
            {
                // Для стабилизации симуляции.
                Thread.Sleep(TimeSpan.FromMilliseconds(1));

                var particles = _particlesService.GetParticles();

                foreach (var particle in particles)
                {
                    BlackHoleViewModel.Instance.BlackHole.Pull(particle);
                    particle.Update();
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        #endregion
    }
}
