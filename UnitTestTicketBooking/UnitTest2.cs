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
    public class UnitTest2
    {
        Train train;
        [TestInitialize]
        public void TestInitialize()
        {
            train = new Train();
        }


        [TestMethod]
        public void CreateTrainListTest()
        {
            train.CreateTrainList();
            int trainCount = train.AvailableTrainList.Count;
            Assert.AreEqual(trainCount, 10);

        }

        [TestMethod]
        public void DisplayTrainTest()
        {
            var trainListReturned = train.DisplayFromJson();
            Assert.IsNotNull(trainListReturned);
        }

            
    }
}
