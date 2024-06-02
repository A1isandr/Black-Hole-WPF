using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Black_Hole.MVVM.Models;
using Black_Hole.MVVM.ViewModels;
using DynamicData;
using ReactiveUI;

namespace Black_Hole.Services
{
    public class ParticlesHistoryService(IParticlesService particleService) : IParticlesHistoryService
    {
        #region Properties

        private readonly SourceList<Vector2> _history = new();

        private IDisposable? _historyThread;

        private const int HistoryCleanUpThreshold = 500;

        private readonly TimeSpan _historyInterval = TimeSpan.FromMilliseconds(100);

        private bool _isRunning;

        #endregion

        #region Methods

        public void Start()
        {
            _historyThread = RxApp.TaskpoolScheduler.Schedule(_historyInterval, () =>
            {
                _isRunning = true;

                while (_isRunning)
                {
                    var particles = particleService.GetParticles().ToList();
                    _history.AddRange(particles.Select(particle => particle.Position));

                    while (_history.Count > HistoryCleanUpThreshold)
                    {
                        _history.RemoveAt(0);
                    }

                    Thread.Sleep(_historyInterval);
                }

            });
        }

        public void Stop()
        {
            _isRunning = false;
            _historyThread?.Dispose();
        }

        public void AddToHistory(Vector2 position)
        {
            if (_history.Count > HistoryCleanUpThreshold)
            {
                _history.RemoveAt(0);
            }

            _history.Add(position);
        }

        public void ClearHistory()
        {
            _history.Clear();
        }

        public IObservable<IChangeSet<Vector2>> Connect() => _history.Connect();

        #endregion
    }
}
