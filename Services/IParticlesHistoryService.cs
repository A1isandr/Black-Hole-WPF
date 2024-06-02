using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DynamicData;

namespace Black_Hole.Services
{
    public interface IParticlesHistoryService
    {
        public void Start();
        
        public void Stop();

        public void AddToHistory(Vector2 position);

        public void ClearHistory();

        public IObservable<IChangeSet<Vector2>> Connect();
    }
}
