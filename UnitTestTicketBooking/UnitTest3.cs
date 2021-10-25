using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        FileManager fileManager;

        [TestInitialize]
        public void TestInitialize()
        {
            fileManager = new FileManager();
        }

        [TestMethod]
        public void FileReadExceptionTest()
        {
            
            string result = fileManager.ReadAllText("NonExistentFileName.json");
            Assert.IsNull(result);

        }

        [TestMethod]
        public void FileWriteExceptionTest()
        {
            string newUser = null ;
            Assert.IsFalse(fileManager.WriteAllText(" ", newUser));

        }

    }
}
