using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
using DynamicData;

namespace Black_Hole.Services
{
    public interface IParticlesService
    {
        public void SpawnParticle(Point coords);

        public void ClearParticles();

        public IEnumerable<Particle> GetParticles();

        public int GetParticlesCount();

        public IObservable<IChangeSet<Particle>> Connect();
    }
}
