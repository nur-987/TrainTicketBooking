using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int NumofTicketsBooked { get; set; }
        public double TotalCost { get; set; }
        public TrainClass TrainClass { get; set; }

        public List<User> UsersList = new List<User>();

        public FileManager FileManager = new FileManager();

        public void InstantiateUser()
        {
            User user = new User()
            {
                UserId = 1,
                UserName = "Genny",

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
                UserName = name,

            };
            userId = user.UserId;
            userlistTemp.Add(user);

            //add to jsonFile
            string userJsonInput = JsonConvert.SerializeObject(userlistTemp);
            FileManager.WriteAllText("User.json", userJsonInput);
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

        public User GetSelectedUserFinalDetail(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("UserName: " + Useritem.UserName);
                Console.WriteLine("Number of Tickets: " + Useritem.NumofTicketsBooked);
                Console.WriteLine("Class: " + Useritem.TrainClass);
                Console.WriteLine("Cost: " + Useritem.TotalCost);
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

    }
}
