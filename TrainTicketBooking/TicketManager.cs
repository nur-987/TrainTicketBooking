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
        public void BuyTicket(int userId, Train selectedTrain, TrainClassEnum selectedClass)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);
            foreach (User item in userlistTemp)
            {
                if (item.UserId == userId)
                {
                    item.TicketHistory.Add(new Ticket() 
                    { BookingTime = DateTime.Now, SelectedClass = selectedClass, SelectedTrain = selectedTrain });
                }
            }
            //updates the trainClass chosen
            var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
            FileManager.WriteAllText("User.json", updatedString);
        }

    }
}
