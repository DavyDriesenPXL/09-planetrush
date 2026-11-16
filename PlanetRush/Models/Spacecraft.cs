using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    public class Spacecraft
    {
        public string Name { get; set; }
        public int FuelCapacity { get; set; }
        public int CargoCapacity { get; set; }
        public int EngineEfficiency { get; set; }
        public bool HasWarpDriveCharge { get; set; }

        private int _currentFuel;

        public int CurrentFuel
        {
            get { return CurrentFuel; }
            set { CurrentFuel = Math.Min(value,FuelCapacity); }
        }

    }
}
