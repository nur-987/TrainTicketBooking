using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public class AppConfiguration : IAppConfiguration
    {
        public int FirstClassBasePrice { get; private set; }
        public int BusinessClassBasePrice { get; private set; }
        public int EconomyClassBasePrice { get; private set; }
        public double FirstClassDistanceMultiplier { get; private set; }
        public double EconomyClassDistanceMultiplier { get; private set; }
        public double BusinessClassDistanceMultiplier { get; private set; }

        public void Initialize(int firstClassBasePrice, int businessClassBasePrice, int economyClassBasePrice,
            double firstClassDistanceMultiplier, double businessClassDistanceMultiplier, double economyClassDistanceMultiplier)
        {
            FirstClassBasePrice = firstClassBasePrice;
            BusinessClassBasePrice = businessClassBasePrice;
            EconomyClassBasePrice = economyClassBasePrice;
            FirstClassDistanceMultiplier = firstClassDistanceMultiplier;
            BusinessClassDistanceMultiplier = businessClassDistanceMultiplier;
            EconomyClassDistanceMultiplier = economyClassDistanceMultiplier;
        }
    }
}
