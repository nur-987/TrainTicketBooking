using System;
namespace TrainTicketBooking
{
    class Program
    {

        static void Main(string[] args)
        {
            UserWorker user = new UserWorker();
            TrainWorker train = new TrainWorker();
            TicketManager ticketManager = new TicketManager();
            bool b = true;

            Console.WriteLine("Welcome to Train Ticket Booking Service");
            while (b)
            {
                int input1 = 0;
                Console.WriteLine("Choose from the following menus");
                Console.WriteLine("1) Buy Tickets");
                Console.WriteLine("2) Purchase History");
                Console.WriteLine("3) Exit ");
                try
                {
                    input1 = Int32.Parse(Console.ReadLine());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Wrong Input. Please choose among the available options.");
                    continue;
                }
                if (input1 == 1)
                {
                    Console.WriteLine("Are you an existing user? (Y/N) "); //check if existing user, else create user
                    string input = Console.ReadLine();
                    int userId = 0;
                    if (string.Equals(input,"N",StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter your name."); //create new user
                        string name = Console.ReadLine();
                        user.AddNewUser(name, out int tempuserId);
                        userId = tempuserId;
                    }
                    else if (string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Enter your user ID");
                        try
                        {
                            userId = Int32.Parse(Console.ReadLine());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Wrong input. Please enter a valid user id");
                            //b = false;
                            break;                          
                        }

                        if (!user.CheckUserExist(userId))
                        {
                            Console.WriteLine("User does not exist. Please select the correct option.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input. Please choose among the available options.");
                        break;
                    }

                    
                    train.DisplayFromJson(); //display all avail trains

                    
                    Console.WriteLine("Which train ticket would u like to purchase? Input ID"); //purchase by train ID
                    int trainId = 0;
                    try
                    {
                        trainId = Int32.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Wrong input. Please choose among the available train id.");
                        break;
                    }
                   
                    ticketManager.BuyTicket(trainId, out int ChosenDist);
                    if (ChosenDist == 0)
                    {
                        Console.WriteLine("Train id does not exist. Please choose among the available train id.");
                        break;
                    }

                    Console.WriteLine("Choose a travel class");
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
                        Console.WriteLine("Wrong input. Please choose among the available travel class.");
                        break;

                    }

                    ticketManager.CalculateBasePrice(tempClass, userId, out int basePrice);
                    if(basePrice == 0)
                    {
                        Console.WriteLine("Wrong input. Please choose among the available travel class.");
                        break;
                    }

                    Console.WriteLine("Enter the total number of tickets to purchase:");


                    int tempNumofTicket = 0;
                    try
                    {
                        tempNumofTicket = Int32.Parse(Console.ReadLine());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Wrong input. PLease enter the correct amount");
                        break;
                    }

                    //subscribes to event
                    //ask user to make payment after completed booking
                    ticketManager.TransactionComplete += Calculation_TransactionComplete;

                    ticketManager.CalculateFinalPrice(basePrice, ChosenDist, tempNumofTicket, userId);

                    Console.WriteLine("Here are your booking details.");
                    user.GetSelectedUserFinalDetail(userId);

                    //after booking complete, exit prog
                    Console.WriteLine("You have completed your booking. Thank you.");
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
                    Console.WriteLine("Wrong Input. Please choose among the available options.");
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
