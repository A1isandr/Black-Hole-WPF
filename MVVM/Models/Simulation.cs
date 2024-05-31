using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Black_Hole.MVVM.ViewModels;

namespace Black_Hole.MVVM.Models
{
    public class Simulation
    {
        public const int C = 30;
        public const float G = 3.54f;
        public const float DeltaTime = 0.01f;

        private static bool IsRunning { get; set; }

        public static void Run()
        {
            IsRunning = true;

            while (IsRunning)
            {
                // Для стабилизации симуляции.
                Thread.Sleep(TimeSpan.FromMilliseconds(1));

                var particles = ParticleCanvasViewModel.Instance.Particles
                    .Select(particle => particle.Particle)
                    .ToList();

                foreach (var particle in particles)
                {
                    particle.Position += particle.Velocity * DeltaTime;
                }
            }
        }

        public static void Stop()
        {
            IsRunning = false;
        }
    }
}
