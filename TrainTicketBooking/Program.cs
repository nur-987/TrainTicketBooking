using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    class Program
    {

        static void Main(string[] args)
        {
            User user = new User();
            Train train = new Train();
            TickerManager tickerManager = new TickerManager();
            bool b = true;

            Console.WriteLine("Welcome to Train Ticket Booking Service");
            while (b)
            {
                //only during first run of prog to input data in
                //train.CreateTrainList();
                //train.AddToJson();
                //user.InstantiateUser();
                //user.InputUserDetails();

                Console.WriteLine("proceed to 1) BUY TICKETS   2) Check PURCHASED TICKET   3)EXIT ");
                int input1 = Int32.Parse(Console.ReadLine());
                if (input1 == 1)
                {
                    //check if existing user
                    //else create user
                    Console.WriteLine("are you a new user? Y/N");
                    string input = Console.ReadLine();
                    int userId = 0;
                    if (input == "Y")
                    {
                        //create new user
                        Console.WriteLine("name?");
                        string name = Console.ReadLine();
                        user.AddNewUser(name, out int tempuserId);
                        userId = tempuserId;

                    }
                    if (input == "N")
                    {
                        Console.WriteLine("userID?");
                        userId = Int32.Parse(Console.ReadLine());
                    }

                    //display all avail trains
                    train.DisplayFromJson();

                    //purchase by train ID
                    Console.WriteLine("Which train ticket would u like to purchase? Input ID");
                    int trainId = Int32.Parse(Console.ReadLine());
                    tickerManager.BuyTicket(trainId, out int ChosenDist);


                    Console.WriteLine("Choose travel class");
                    Console.WriteLine("1) " + TrainClass.FirstClass.ToString());
                    Console.WriteLine("2) " + TrainClass.BusinessClass.ToString());
                    Console.WriteLine("3) " + TrainClass.Economy.ToString());
                    int tempClass = Int32.Parse(Console.ReadLine());
                    tickerManager.CalculateBasePrice(tempClass, userId, out int basePrice);

                    Console.WriteLine("How many tickets?");
                    int tempNumofTicket = Int32.Parse(Console.ReadLine());

                    //raise event
                    //ask user to make payment after completed booking
                    tickerManager.TransactionComplete += tickerManager.Calculation_TransactionComplete;

                    tickerManager.CalculateFinalPrice(basePrice, ChosenDist, tempNumofTicket, userId);

                    user.GetSelectedUserFinalDetail(userId);

                    //after booking complete, exit prog
                    Console.WriteLine("thankyou for making a booking");
                    b = false;

                    //check
                    //user.GetAllUser();
                }
                else if (input1 == 2)
                {
                    Console.WriteLine("userID?");
                    int userId = Int32.Parse(Console.ReadLine());
                    user.GetSelectedUserFinalDetail(userId);
                }
                else if (input1 == 3)
                {
                    Console.WriteLine("exiting program. Good Bye");
                    b = false;
                }
                
            }

            Console.ReadLine();

        }

    }
}
