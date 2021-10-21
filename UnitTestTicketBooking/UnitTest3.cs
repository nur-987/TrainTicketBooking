using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicketBooking;

namespace UnitTestTicketBooking
{
    [TestClass]
    public class UnitTest3
    {
        TicketManager ticketManager;
        Train train;

        [TestInitialize]
        public void TestInitialize()
        {
            ticketManager = new TicketManager();
            train = new Train();

        }

        [TestMethod]
        public void BuyTicketTest()
        {
            ticketManager.BuyTicket(1, out int distance);
            Assert.AreEqual(204, distance);
        }

        [TestMethod]
        public void CalculateBasePriceTest()
        {
            int basePriceFirstClass = 300;
            ticketManager.CalculateBasePrice(0, 4, out int basePrice);
            
            Assert.IsNotNull(basePrice);
            Assert.AreEqual(basePriceFirstClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 4);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("FirstClass", x);
            }

        }

        [TestMethod]
        public void CalculateBasePriceTest2()
        {
            int basePriceBisClass = 250;
            ticketManager.CalculateBasePrice(1, 4, out int basePrice);

            Assert.IsNotNull(basePrice);
            Assert.AreEqual(basePriceBisClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 4);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("BusinessClass", x);
            }

        }

        [TestMethod]
        public void CalculateBasePriceTest3()
        {
            int basePriceEconClass = 150;
            ticketManager.CalculateBasePrice(2, 4, out int basePrice);

            Assert.IsNotNull(basePrice);
            Assert.AreEqual(basePriceEconClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 4);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("Economy", x);
            }

        }

        [TestMethod]
        public void CalculateFinalPriceTest()
        {
            ticketManager.CalculateFinalPrice(300, 204, 1, 4);

            double rate = 1.5;
            double expectedPrice = 300 + (204 * rate);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 4);
            if (user != null)
            {
                int x = user.NumofTicketsBooked;
                Assert.AreEqual(1, x);
                double y = user.TotalCost;
                Assert.AreEqual(expectedPrice, y);
            }

        }

        [TestMethod]
        public void TransactionCompleteEventTest()
        {
            ticketManager.CalculateFinalPrice(300, 204, 1, 4);
            double totalCost = 606;
            ticketManager.TransactionComplete += TicketManager_TransactionComplete;
        }

        private void TicketManager_TransactionComplete(double totalCost)
        {
            throw new NotImplementedException();
        }
    }
}
