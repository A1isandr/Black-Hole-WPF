using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Hole.Services
{
    public interface ISimulationService
    {
        public float C { get; set; }

        public float G { get; set; }

        public float DeltaTime { get; set; }

        public bool IsRunning { get; set; }

        public void Run();
        public void Stop();
    }
}
