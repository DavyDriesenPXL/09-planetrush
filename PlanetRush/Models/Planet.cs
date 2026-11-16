using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Models
{
    public class Planet
    {
        public int Radius { get; set; }
        public int Seed { get; set; }
        public string Name { get; set; }
        public int TonsOfRawAetherium { get; set; }
        public int TonsOfTrilliumAlloys { get; set; }
        public bool NeedsTerraforming { get; set; }
        public bool NeedsGenestealing { get; set; }
        public bool NeedsNuking { get; set; }
        public int NumberOfLightYearsToReach { get; set; }

        private Random _rng;

        public double ResourceDensity
        {
            get
            { return (double)(TonsOfRawAetherium + TonsOfTrilliumAlloys) / 10; }
        }

        public Planet(int galaxyCode)
        {
            _rng = new();
            Radius = _rng.Next(4, 14);
            Seed = _rng.Next();
            Name = GeneratePlanetCode(galaxyCode);
            TonsOfRawAetherium = _rng.Next(10, 111);
            TonsOfTrilliumAlloys = _rng.Next(-10, 31);

            if (TonsOfTrilliumAlloys < 0)
            {
                TonsOfTrilliumAlloys = 0;
            }

            NumberOfLightYearsToReach = _rng.Next(5, 21);
            ConfigureCorrectOption();
        }

        public Planet() : this(0)
        {

        }

        private void ConfigureCorrectOption()
        {
            int random = _rng.Next(3);
            if (random == 0)
            {
                NeedsGenestealing = true;
            }
            else if (random == 1)
            {
                NeedsTerraforming = true;
            }
            else if (random == 2)
            {
                NeedsNuking = true;
            }
        }

        private string GeneratePlanetCode(int galaxyCode = 0)
        {
            string prefix = "";
            int number = _rng.Next(1000, 10000);

            string quadrant = "";
            string quadrantPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (galaxyCode == 1)
            {
                prefix += "M31";
            }
            else if (galaxyCode == 2)
            {
                prefix += "M33";
            }
            else
            {
                prefix += "MW";
            }

            for (int i = 0; i < 2; i++)
            {
                int index = _rng.Next(quadrantPool.Length);
                quadrant += quadrantPool[index];
            }

            return $"{prefix}-{quadrant}-{number}";
        }
    }
}

