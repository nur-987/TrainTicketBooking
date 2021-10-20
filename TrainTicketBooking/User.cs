using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int NumofTicketsBooked { get; set; }
        public double TotalCost { get; set; }
        public TrainClass TrainClass { get; set; }

        public List<User> UsersList = new List<User>();


        public void InstantiateUser()
        {
            User user = new User()
            {
                UserId = 1,
                UserName = "Genny",

            };
            UsersList.Add(user);
        }
        public void AddNewUser(string name, out int userId)
        {
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("User.json"));

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
            File.WriteAllText("User.json", userJsonInput);
        }
        public void InputUserDetails()
        {
            //from List => JsonFile
            string userJsonInput = JsonConvert.SerializeObject(UsersList);
            File.WriteAllText("User.json", userJsonInput);

        }

        public void GetAllUser()
        {
            //From JsonFile => Code
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("User.json"));
            foreach (User item in userlistTemp)
            {
                Console.WriteLine("ID: " + item.UserId);
                Console.WriteLine("UserName: " + item.UserName);
                Console.WriteLine("NumofTickets: " + item.NumofTicketsBooked);
                Console.WriteLine("Class: " + item.TrainClass);
                Console.WriteLine("Cost: " + item.TotalCost);

            }
        }

        public void GetSelectedUserFinalDetail(int userId)
        {
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("User.json"));
            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("UserName: " + Useritem.UserName);
                Console.WriteLine("Number of Tickets: " + Useritem.NumofTicketsBooked);
                Console.WriteLine("Class: " + Useritem.TrainClass);
                Console.WriteLine("Cost: " + Useritem.TotalCost);
            }

        }

    }
}
