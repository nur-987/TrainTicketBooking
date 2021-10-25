using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrainTicketBooking;

namespace UnitTestTicketBooking
{
    [TestClass]
    public class UnitTest4
    {
        TicketManager ticketManager;
        bool isRaised = false;

        [TestInitialize]
        public void TestInitialize()
        {
            ticketManager = new TicketManager();
           
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
            ticketManager.CalculateBasePrice(1, 1, out int basePrice);
            int basePriceFirstClass = 300;

            Assert.AreEqual(basePriceFirstClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 1);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("FirstClass", x);
            }
        }

        [TestMethod]
        public void CalculateBasePriceTest2()
        {
            ticketManager.CalculateBasePrice(2, 1, out int basePrice);
            int basePriceBusinessClass = 250;

            Assert.AreEqual(basePriceBusinessClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 1);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("BusinessClass", x);
            }

        }

        [TestMethod]
        public void CalculateBasePriceTest3()
        {
            ticketManager.CalculateBasePrice(3, 1, out int basePrice);
            int basePriceEconClass = 150;

            Assert.AreEqual(basePriceEconClass, basePrice);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 1);
            if (user != null)
            {
                string x = user.TrainClass.ToString();
                Assert.AreEqual("Economy", x);
            }
        }

        [TestMethod]
        public void CalculateFinalPriceTest()
        {
            ticketManager.CalculateFinalPrice(300, 204, 1, 2);

            double rate = 1.5;
            double expectedPrice = 300 + (204 * rate);

            var userInfo = File.ReadAllText("User.json");
            List<User> userListJson = JsonConvert.DeserializeObject<List<User>>(userInfo);
            var user = userListJson.First(x => x.UserId == 2);
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
            ticketManager.TransactionComplete += TicketManager_TransactionComplete;
            ticketManager.CalculateFinalPrice(300, 204, 1, 2);
            Assert.IsTrue(isRaised);
        }
        private void TicketManager_TransactionComplete(double totalCost)
        {
            isRaised = true;
            
        }

        [TestCleanup]
        public void CodeCleanUp()
        {

        }
    }
}
