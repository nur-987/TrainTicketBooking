using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainTicketBooking
{
    public class UserWorker
    {
        public List<User> userList = new List<User>();

        public FileManager FileManager = new FileManager();

        public void AddFirstUser()
        {
            User user1 = new User()
            {
                UserId = 1,
                Name = "Genny",

            };
            userList.Add(user1);

            string userJsonInput = JsonConvert.SerializeObject(userList);
            FileManager.WriteAllText("User.json", userJsonInput);
        }
        public void Initialize()
        {
            if (!File.Exists("User.json"))
            {
                User user = new User()
                {
                    UserId = 1,
                    Name = "Genny",
                    TicketHistory = new List<Ticket>()                  
                    
                };

                userList.Add(user);
                //from List => JsonFile
                string userJsonInput = JsonConvert.SerializeObject(userList);
                FileManager.WriteAllText("User.json", userJsonInput);
            }

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
                TicketHistory = new List<Ticket>()
            };
            userId = user.UserId;
            userlistTemp.Add(user);

            //add to jsonFile
            string userJsonInput = JsonConvert.SerializeObject(userlistTemp);
            FileManager.WriteAllText("User.json", userJsonInput);
        }

        public void GetSelectedUserFinalDetail(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("Name: " + Useritem.Name);

                //choose which history to display
                int counter = Useritem.TicketHistory.Count; //choosing the last item in history

                //foreach ticket = print ticket history
                foreach (var ticketHistoryItem in Useritem.TicketHistory)
                {
                    if (ticketHistoryItem.TicketId == counter)
                    {
                        Console.WriteLine("Booking Time: " + ticketHistoryItem.BookingTime);
                        Console.WriteLine("Origin Station: " + ticketHistoryItem.SelectedTrain.StartDestination);
                        Console.WriteLine("End Station: " + ticketHistoryItem.SelectedTrain.EndDestination);
                        Console.WriteLine("Departure Time: " + ticketHistoryItem.SelectedTrain.DepartureTime.ToShortTimeString());
                        Console.WriteLine("Arrival Time: " + ticketHistoryItem.SelectedTrain.ArrivalTime.ToShortTimeString());
                        Console.WriteLine("Number of tickets: " + ticketHistoryItem.NumOfTickets);
                        Console.WriteLine("Travel Class: " + ticketHistoryItem.SelectedClass);

                    }
                    
                }

            }

        }

        public void GetSelectedUserAllDetails(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("Name: " + Useritem.Name);

                //foreach ticket = print ticket history
                foreach (var ticketHistoryItem in Useritem.TicketHistory)
                {
                    Console.WriteLine("Ticket Id: " + ticketHistoryItem.TicketId);
                    Console.WriteLine("Booking Time: " + ticketHistoryItem.BookingTime);
                    Console.WriteLine("Origin Station: " + ticketHistoryItem.SelectedTrain.StartDestination);
                    Console.WriteLine("End Station: " + ticketHistoryItem.SelectedTrain.EndDestination);
                    Console.WriteLine("Departure Time: " + ticketHistoryItem.SelectedTrain.DepartureTime.ToShortTimeString());
                    Console.WriteLine("Arrival Time: " + ticketHistoryItem.SelectedTrain.ArrivalTime.ToShortTimeString());
                    Console.WriteLine("Number of tickets: " + ticketHistoryItem.NumOfTickets);
                    Console.WriteLine("Travel Class: " + ticketHistoryItem.SelectedClass);
                    Console.WriteLine("-------------------------------------");
                }

            }

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
