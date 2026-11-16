using System;
using System.Collections.Generic;
using System.Linq;
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

        private Random rnd;

        public double ResourceDensity 
        { get
            { return (TonsOfRawAetherium + TonsOfTrilliumAlloys) / 10; }
        }

        public Planet()
        {
            
        }

        public Planet(int galaxyCode)
        {
            rnd = new();
            Radius = rnd.Next(4, 14);
            Seed = rnd.Next();
            Name = GeneratePlanetCode(galaxyCode);
            TonsOfRawAetherium = rnd.Next(10, 111);
            TonsOfTrilliumAlloys = Math.Abs(rnd.Next(-10, 31));
            NumberOfLightYearsToReach = rnd.Next(5, 21);
            ConfigureCorrectOption();
        }

        private void ConfigureCorrectOption()
        {
            int random = rnd.Next(1, 4);
            if(random == 1)
            {
                NeedsGenestealing = true;
            }
            else if(random == 2)
            {
                NeedsTerraforming = true;
            }
            else if(random == 3)
            {
                NeedsNuking = true;
            }
        }
    }
}
