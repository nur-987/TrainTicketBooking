using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TrainTicketBooking
{
    public delegate void TransactionAlert(double totalCost);
    public class TicketManager
    {
        IAppConfiguration _config;
        public TicketManager(IAppConfiguration config)
        {
            _config = config;
        }

        FileManager FileManager = new FileManager();

        public event TransactionAlert TransactionComplete;
        public void BuyTicket(int userId, Train selectedTrain, int selectedClass)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);
            foreach (User item in userlistTemp)
            {
                if (item.UserId == userId)
                {
                    try
                    {
                        item.TicketHistory.Add(new Ticket()
                        {
                            BookingTime = DateTime.Now,
                            SelectedClass = (TrainClassEnum)selectedClass,
                            SelectedTrain = selectedTrain
                        });
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("error");
                    }
                    finally
                    {
                        //updates the ticket bought
                        var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
                        FileManager.WriteAllText("User.json", updatedString);
                    }
                }
            }

            ////updates the ticket bought
            //var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
            //FileManager.WriteAllText("User.json", updatedString);
        }



    }
}
