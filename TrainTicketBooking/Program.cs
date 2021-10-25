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
            TicketManager ticketManager = new TicketManager();
            bool b = true;

            Console.WriteLine("Welcome to Train Ticket Booking Service");
            while (b)
            {
                //only during first run of prog to input data in
                //train.CreateTrainList();
                //train.AddToJson();
                //user.InstantiateUser();
                //user.InputUserDetails();

                int input1 = 0;
                Console.WriteLine("proceed to 1)BUY TICKETS   2)CHECK PURCHASED TICKET   3)EXIT ");
                try
                {
                    input1 = Int32.Parse(Console.ReadLine());
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error. {ex.Message}");
                    Console.WriteLine("try again");
                    break;
                }
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
                        try
                        {
                            userId = Int32.Parse(Console.ReadLine());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Error. {ex.Message}");
                            Console.WriteLine("try again");
                            //b = false;
                            break;                          
                        }

                        if (!user.CheckUserExist(userId))
                        {
                            Console.WriteLine("user does not exist!");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("wrong input. restarting system");
                        break;
                    }

                    //display all avail trains
                    train.DisplayFromJson();

                    //purchase by train ID
                    Console.WriteLine("Which train ticket would u like to purchase? Input ID");
                    int trainId = 0;
                    try
                    {
                        trainId = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error. {ex.Message}");
                        Console.WriteLine("exiting system");
                        break;
                    }
                   
                    ticketManager.BuyTicket(trainId, out int ChosenDist);
                    if (ChosenDist == 0)
                    {
                        Console.WriteLine("train id does not exist. Please retry");
                        break;
                    }

                    Console.WriteLine("Choose travel class");
                    Console.WriteLine("1) " + TrainClass.FirstClass.ToString());
                    Console.WriteLine("2) " + TrainClass.BusinessClass.ToString());
                    Console.WriteLine("3) " + TrainClass.Economy.ToString());

                    int tempClass = 0;
                    try
                    {
                        tempClass = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error. {ex.Message}");
                        Console.WriteLine("exiting system");
                        break;

                    }

                    ticketManager.CalculateBasePrice(tempClass, userId, out int basePrice);
                    if(basePrice == 0)
                    {
                        Console.WriteLine("wrong input. please try again.");
                        break;
                    }

                    Console.WriteLine("How many tickets?");


                    int tempNumofTicket = 0;
                    try
                    {
                        tempNumofTicket = Int32.Parse(Console.ReadLine());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error. {ex.Message}");
                        Console.WriteLine("exiting system");
                        break;
                    }

                    //raise event
                    //ask user to make payment after completed booking
                    ticketManager.TransactionComplete += Calculation_TransactionComplete;

                    ticketManager.CalculateFinalPrice(basePrice, ChosenDist, tempNumofTicket, userId);

                    user.GetSelectedUserFinalDetail(userId);

                    //after booking complete, exit prog
                    Console.WriteLine("thankyou for making a booking");
                    b = false;

                }
                else if (input1 == 2)
                {
                    Console.WriteLine("userID?");
                    int userId = 0;
                    try
                    {
                        userId = Int32.Parse(Console.ReadLine());
                        user.GetSelectedUserFinalDetail(userId);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error. {ex.Message}");
                        Console.WriteLine("exit application");
                        b = false;
                    }

                }
                else if (input1 == 3)
                {
                    Console.WriteLine("exiting program. Good Bye");
                    b = false;
                }
                else
                {
                    Console.WriteLine("wrong input. try again");
                }
                
            }


            Console.ReadLine();

        }
        private static void Calculation_TransactionComplete(double totalCost)
        {
            Console.WriteLine("Please make payment of $" + totalCost + " within 24 hours");
            Console.WriteLine("proceed to payment at: www.trainticketpayment.com");
        }

    }
}
