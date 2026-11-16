using PlanetRush.Sounds;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

            if (chance == 0)
            {
                DesiredMetal = TrilliumAlloys;
                FuelForMetalRate = _rng.Next(10, 20);
            }
            if (chance == 1)
            {
                DesiredMetal = RawAetherium;
                FuelForMetalRate = _rng.Next(2, 7);
            }

            if (_rng.Next(1, 11) == 1)
            {
                HasWarpDriveChargesForSale = true;
            }

            Planet = planet;
        }

        public void Visit(Player player)
        {
            int menuChoice;
            bool isCorrect;

            Console.WriteLine(@$"The alien-looking trader greets you as you enter his home.
The station is crawling with robotic servants.

    'Welcome in the Trader Station of {Planet.Name}.
     I'm looking to buy {DesiredMetal}. Do you have any?
     I can trade you for it, fellow space explorer.'
");

            string traderStationOptions = $" 1. Sell {DesiredMetal} for Fuel ({FuelForMetalRate} per ton of {DesiredMetal})\n" +
" 2. Leave and travel to the next planet\n" +
" 3. Look at your travel logs";

            string leaveTraderStation = "You leave the safety of the trader station and journey back into the eternal void of deepspace.";

            do
            {
                Console.WriteLine(traderStationOptions);

                do
                {
                    isCorrect = int.TryParse(Console.ReadLine(), out menuChoice) && (menuChoice == 1 || menuChoice == 2 || menuChoice == 3);
                    if (!isCorrect)
                    {
                        Console.Write("Choose a valid option instead: ");
                    }
                } while (!isCorrect && menuChoice != 2);

                switch (menuChoice)
                {
                    case 1:
                        SellMetals(player);
                        break;
                    case 2:
                        Console.WriteLine(leaveTraderStation);
                        break;
                    case 3:
                        break;
                }
            } while (menuChoice != 2);
        }

        private void SellMetals(Player player)
        {
            int carriedTons = DesiredMetal == TrilliumAlloys ? player.TonsOfTrilliumAlloys : player.TonsOfRawAetherium;
            Console.WriteLine("How many tons do you sell?");
            Console.WriteLine($"You currently carry {carriedTons} tons.");
            Console.WriteLine($"Your spacecraft has {player.Spacecraft.CurrentFuel}/{player.Spacecraft.FuelCapacity} liters.");
            int chosenAmount;
            while (!int.TryParse(Console.ReadLine(), out chosenAmount) || chosenAmount > carriedTons || chosenAmount < 0)
            {
                SoundBoard.PlayFailSound();
                Console.Write("Chose a valid amount instead: ");
            }
            if (DesiredMetal == TrilliumAlloys)
            {
                player.TonsOfTrilliumAlloys -= chosenAmount;
            }
            else
            {
                player.TonsOfRawAetherium -= chosenAmount;
            }

            player.AddFuel(chosenAmount * FuelForMetalRate);
            SoundBoard.PlayConfirmSound();
            Console.WriteLine($"You sold {chosenAmount} tons of metals and bought {chosenAmount * FuelForMetalRate} liters of fuel");
        }
    }
}
