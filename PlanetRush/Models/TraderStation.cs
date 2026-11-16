using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetRush.Models
{
    public class TraderStation
    {
        private const string TrilliumAlloys = "Trillium Alloys";
        private const string RawAetherium = "Raw Aetherium";

        private string _desiredMetal;

        public string DesiredMetal
        {
            get { return _desiredMetal; }
            set
            {
                if (_desiredMetal.Equals(TrilliumAlloys) || _desiredMetal.Equals(RawAetherium))
                {
                    _desiredMetal = value;
                }
                else
                {
                    _desiredMetal = "NONE";
                }
            }
        }

        private Random _rng;

        public bool HasWarpDriveChargesForSale { get; set; }
        public int FuelForMetalRate { get; set; }
        public Planet Planet { get; set; }

        public TraderStation(Planet planet)
        {
            _rng = new();
            int chance = _rng.Next(2);

            if(chance == 0)
            {
                DesiredMetal = TrilliumAlloys;
                FuelForMetalRate = _rng.Next(10, 20);
            }
            if(chance == 1)
            {
                DesiredMetal = RawAetherium;
                FuelForMetalRate = _rng.Next(2, 7);
            }
           
            if(_rng.Next(1,11) == 1)
            {
                HasWarpDriveChargesForSale = true;
            }

            Planet = planet;
        }
    }
}
