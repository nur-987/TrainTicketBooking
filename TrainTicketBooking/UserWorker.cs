using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainTicketBooking
{
    public class UserWorker
    {
        public List<User> UsersList = new List<User>();

        public FileManager FileManager = new FileManager();

        public void InstantiateUser()
        {
            User user = new User()
            {
                UserId = 1,
                Name = "Genny",

            };
            UsersList.Add(user);

            //from List => JsonFile
            string userJsonInput = JsonConvert.SerializeObject(UsersList);
            FileManager.WriteAllText("User.json", userJsonInput);
        }

        public void AddNewUser(string name, out int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            int num = userlistTemp.Count();
            User user = new User()
            {
                UserId = num + 1,
                Name = name,

            };
            userId = user.UserId;
            userlistTemp.Add(user);

            //add to jsonFile
            string userJsonInput = JsonConvert.SerializeObject(userlistTemp);
            FileManager.WriteAllText("User.json", userJsonInput);
        }

        public User GetSelectedUserFinalDetail(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("UserName: " + Useritem.Name);
                //foreach ticket = print ticket history
            }
            return Useritem;

        }

        public bool CheckUserExist(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            foreach (User item in userlistTemp)
            {
                if (item.UserId == userId)
                {
                    return true;
                }
            }

            return false;
        }

        #region 
        //public List<User> GetAllUser()
        //{
        //    //From JsonFile => Code
        //    string userFromJson = FileManager.ReadAllText("User.json");
        //    List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);
        //    foreach (User item in userlistTemp)
        //    {
        //        Console.WriteLine("ID: " + item.UserId);
        //        Console.WriteLine("UserName: " + item.UserName);
        //        Console.WriteLine("NumofTickets: " + item.NumofTicketsBooked);
        //        Console.WriteLine("Class: " + item.TrainClass);
        //        Console.WriteLine("Cost: " + item.TotalCost);

        //    }
        //    return userlistTemp;
        //}
        #endregion
    }
}
