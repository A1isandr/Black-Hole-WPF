﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using DynamicData;
using ReactiveUI;

namespace Black_Hole.Services
{
    public class ParticlesService : IParticlesService
    {
        #region Properties

        private readonly SourceList<Particle> _particles = new();

        private  readonly List<Particle> _deleteCandidates = [];

        #endregion

        #region Methods

        public void SpawnParticle(Point coords)
        {
            var particle = new Particle
            (
                (SimulationService)App.ServiceProvider!.GetService(typeof(ISimulationService))!, 
                new Vector2((float)coords.X, (float)coords.Y)
            );
            _particles.Add(particle);
        }

        public void AddToDeleteCandidates(Particle particle)
        {
            _deleteCandidates.Add(particle);
        }

        public void DeleteDeleteCandidates()
        {
            _particles.RemoveMany(_deleteCandidates);
        }

        public void DeleteAllParticles()
        {
            _particles.Clear();
        }

        public IEnumerable<Particle> GetParticles()
        {
            return _particles.Items;
        }

        public int GetParticlesCount()
        {
            return _particles.Count;
        }

        public IObservable<IChangeSet<Particle>> Connect() => _particles.Connect();

        #endregion
    }
}
