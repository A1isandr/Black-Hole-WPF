using Black_Hole.MVVM.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Black_Hole.Services
{
    public class SimulationService(IParticlesService particlesService) : ISimulationService
    {

        #region SimulationService Properties

        [Reactive]
        public float C { get; set; } = 30f;

        [Reactive]
        public float G { get; set; } = 3f;

        [Reactive]
        public float DeltaTime { get; set; } = 0.3f;

        #endregion

        #region Properties

        public bool IsRunning { get; set; }

        #endregion

        #region Methods

        public void Run()
        {
            IsRunning = true;

            while (IsRunning)
            {
                // Для стабилизации симуляции.
                Thread.Sleep(TimeSpan.FromMilliseconds(1));

                var particles = particlesService.GetParticles();

                foreach (var particle in particles)
                {
                    BlackHoleViewModel.Instance.BlackHole.Pull(particle);
                    particle.Update();
                }

                particlesService.DeleteDeleteCandidates();
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }

        #endregion

    }
}
