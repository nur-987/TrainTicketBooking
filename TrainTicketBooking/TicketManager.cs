using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrainTicketBooking
{
    delegate void TransactionAlert(double totalCost);
    class TickerManager
    {
        public event TransactionAlert TransactionComplete;
        public void BuyTicket(int trainId, out int ChosenDist)
        {
            //getting the distance
            int distance = 0;

            List<Train> trainlistJson = JsonConvert.DeserializeObject<List<Train>>(File.ReadAllText("TrainList.json"));
            foreach (Train item in trainlistJson)
            {
                if (item.TrainId == trainId)
                {
                    distance = item.Distance;

                }

            }
            ChosenDist = distance;

            //future additions
            //remove seat from availabe seats
            //provide seat number

        }

        public void CalculateBasePrice(int enumIndex, int userId, out int basePrice)
        {
            //based on class chosen
            //adding the class chosen to user.cs
            basePrice = 0;

            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("User.json"));
            foreach (User item in userlistTemp)
            {
                if (item.UserId == userId)
                {
                    if (enumIndex == 1)
                    {
                        item.TrainClass = TrainClass.FirstClass;
                        int basePrice1 = 300;
                        basePrice = basePrice1;
                    }
                    if (enumIndex == 2)
                    {
                        item.TrainClass = TrainClass.BusinessClass;
                        int basePrice2 = 250;
                        basePrice = basePrice2;
                    }
                    if (enumIndex == 3)
                    {
                        item.TrainClass = TrainClass.Economy;
                        int basePrice3 = 150;
                        basePrice = basePrice3;
                    }
                }

                #region old code without json file
                //    foreach (User useritem in user.UsersList)
                //{
                //    if (useritem.UserId == userId)
                //    {
                //        if (enumIndex == 1)
                //        {
                //            useritem.TrainClass = MyClass.FirstClass;
                //            int basePrice1 = 300;
                //            basePrice = basePrice1;
                //        }
                //        if (enumIndex == 2)
                //        {
                //            useritem.TrainClass = MyClass.BussinessClass;
                //            int basePrice2 = 250;
                //            basePrice = basePrice2;
                //        }
                //        if (enumIndex == 3)
                //        {
                //            useritem.TrainClass = MyClass.Economy;
                //            int basePrice3 = 150;
                //            basePrice = basePrice3;
                //        }
                //    }
                //}
                #endregion

            }
            //updates the trainClass chosen
            var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
            File.WriteAllText("User.json", updatedString);

        }

        public void CalculateFinalPrice(int basePrice, int ChosenDist, int numOfTickets, int userId)
        {
            double rate = 1.5;
            double totalPerTicket = basePrice + (ChosenDist * rate);
            double grandTotal = totalPerTicket * numOfTickets;

            Console.WriteLine("final bill is:$ " + grandTotal);

            #region old code without json
            //foreach (User item in user.UsersList)
            //{
            //    if (item.UserId == userId)
            //    {
            //        item.NumofTicketsBooked = numOfTickets;
            //        item.TotalCost = grandTotal;
            //    }
            //}
            #endregion

            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("User.json"));

            //linQ version; optimised search
            var user = userlistTemp.First(x => x.UserId == userId);
            if (user != null)
            {
                user.NumofTicketsBooked = numOfTickets;
                user.TotalCost = grandTotal;
            }

            //updates the numOfTickets and totalCost
            var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
            File.WriteAllText("User.json", updatedString);

            //get user to make payment
            if (TransactionComplete != null)
            {
                TransactionComplete.Invoke(grandTotal);
            }

        }

        public void Calculation_TransactionComplete(double totalCost)
        {
            Console.WriteLine("Please make payment of $" + totalCost + " within 24 hours");
            Console.WriteLine("proceed to payment at: www.trainticketpayment.com");
        }
    }
}
