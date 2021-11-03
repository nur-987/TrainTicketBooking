using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void BuyTicket(int userId, Train selectedTrain, TrainClassEnum selectedClass, int numofTickets)
        {
            
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);
            foreach (User item in userlistTemp)
            {
                int idCount = item.TicketHistory.Count;
                if (item.UserId == userId)
                {
                    try
                    {
                        item.TicketHistory.Add(new Ticket()
                        {
                            TicketId = ++idCount,
                            BookingTime = DateTime.Now,
                            SelectedClass = selectedClass,
                            SelectedTrain = selectedTrain,
                            NumOfTickets = numofTickets
                        });

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Unable to add train to purchase");
                    }
                    finally
                    {
                        //updates the ticket bought
                        //update the json file
                        var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
                        FileManager.WriteAllText("User.json", updatedString);
                    }

                }
            }

        }

        public void GrandTotal(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            foreach (User userItem in userlistTemp)
            {
                //choose which history to display
                int counter = userItem.TicketHistory.Count;

                if (userItem.UserId == userId && userItem.TicketHistory != null)
                {
                        int quantity = 0;
                        double price = 0;

                        foreach (var itemInTicketHist in userItem.TicketHistory)
                        {
                        if (itemInTicketHist.TicketId == counter)
                        {
                            quantity = itemInTicketHist.NumOfTickets;

                            if (itemInTicketHist.SelectedClass == TrainClassEnum.FirstClass)
                            {
                                price = itemInTicketHist.SelectedTrain.FirstClassFare;
                            }
                            else if (itemInTicketHist.SelectedClass == TrainClassEnum.BusinessClass)
                            {
                                price = itemInTicketHist.SelectedTrain.BusinessClassFare;
                            }
                            else if (itemInTicketHist.SelectedClass == TrainClassEnum.Economy)
                            {
                                price = itemInTicketHist.SelectedTrain.EconomyClassFare;
                            }

                        }
                        }

                        double finalCost = quantity * price;
                        Console.WriteLine("Total Cost: $" + finalCost);

                    if (TransactionComplete != null)
                        {
                            TransactionComplete.Invoke(finalCost);
                        }
                    }
                
            }

   
        }



    }
}
