using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TrainTicketBooking;

namespace UnitTestTicketBooking
{
    [TestClass]
    public class UnitTest1
    {
        User user;

        [TestInitialize]
        public void TestInitialize()
        {
            user = new User();
            var fileMock = new Mock<IFileReadWrite>(MockBehavior.Strict);
            user.FileManager = fileMock.Object;

            string usersInJsonFile = @"[{
             ""UsersList"": [],""UserId"": 1, ""UserName"": ""Charlie"",""NumofTicketsBooked"": 4, ""TotalCost"": 2578.0,""TrainClass"": 1
             },{
             ""UsersList"": [],""UserId"": 2, ""UserName"": ""Ben"",""NumofTicketsBooked"": 3, ""TotalCost"": 1363.5,""TrainClass"": 2
             }]";

            string usersToUpdateInJsonFile = @"[{
             ""UsersList"": [],""UserId"": 1, ""UserName"": ""Charlie"",""NumofTicketsBooked"": 4, ""TotalCost"": 2578.0,""TrainClass"": 1
             },{
             ""UsersList"": [],""UserId"": 2, ""UserName"": ""Ben"",""NumofTicketsBooked"": 3, ""TotalCost"": 1363.5,""TrainClass"": 2
             },""UsersList"": [],""UserId"": 3, ""UserName"": ""Kelly"",""NumofTicketsBooked"": 0, ""TotalCost"": 0,""TrainClass"": 0
             }]"; 

            fileMock.Setup(x => x.ReadAllText("User.json")).Returns(usersInJsonFile);
            fileMock.Setup(x => x.WriteAllText("User,json", usersToUpdateInJsonFile));
            
        }

        [TestMethod]
        public void InstantiateUserTest()
        {
            user.InstantiateUser();
            int count = user.UsersList.Count;
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void AddNewUserTest()
        {
            user.AddNewUser("Jack", out int userId);

        }
    }
}
