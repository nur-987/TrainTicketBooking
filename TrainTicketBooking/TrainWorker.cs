using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public class TrainWorker
    {
        public FileManager FileManager = new FileManager();
        private List<Train> _trainlistJson;
        IAppConfiguration _config;
        public TrainWorker(IAppConfiguration config)        //interface boxing; allow for mocking
        {
            _config = config;
        }
        public void CreateTrainList()
        {
            Train train1 = new Train()
            {
                TrainId = 1,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 8, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 10, 00, 00)

            };
            Train train2 = new Train()
            {
                TrainId = 2,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 20, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train3 = new Train()
            {
                TrainId = 3,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 9, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 12, 00, 00)

            };
            Train train4 = new Train()
            {
                TrainId = 4,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 19, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 21, 30, 00)

            };
            Train train5 = new Train()
            {
                TrainId = 5,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00)

            };
            Train train6 = new Train()
            {
                TrainId = 6,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 02, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 06, 00, 00)

            };
            Train train7 = new Train()
            {
                TrainId = 7,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 17, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 45, 00)

            };
            Train train8 = new Train()
            {
                TrainId = 8,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 07, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 09, 15, 00)

            };
            Train train9 = new Train()
            {
                TrainId = 9,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 18, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train10 = new Train()
            {
                TrainId = 10,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 03, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 07, 00, 00),

            };

            List<Train> AvailableTrainList = new List<Train>();
            AvailableTrainList.Add(train1);
            AvailableTrainList.Add(train2);
            AvailableTrainList.Add(train3);
            AvailableTrainList.Add(train4);
            AvailableTrainList.Add(train5);
            AvailableTrainList.Add(train6);
            AvailableTrainList.Add(train7);
            AvailableTrainList.Add(train8);
            AvailableTrainList.Add(train9);
            AvailableTrainList.Add(train10);

            //add to json file
            string trainListJson = JsonConvert.SerializeObject(AvailableTrainList);
            FileManager.WriteAllText("TrainList.json", trainListJson);

        }

        public void Initialize()
        {
            if (!File.Exists("TrainList.json"))
            {
                CreateTrainList();
            }  
            string trainFromJson = FileManager.ReadAllText("TrainList.json");
            _trainlistJson = JsonConvert.DeserializeObject<List<Train>>(trainFromJson);
            foreach (Train train in _trainlistJson)
            {
                train.BusinessClassFare = _config.BusinessClassBasePrice + train.Distance * _config.BusinessClassDistanceMultiplier;
                train.EconomyClassFare = _config.EconomyClassBasePrice + train.Distance * _config.EconomyClassDistanceMultiplier;
                train.FirstClassFare = _config.FirstClassBasePrice + train.Distance * _config.FirstClassDistanceMultiplier;
            }
        }

        public List<string> GetAllSourceStations()
        {
            List<string> sourceStationList = new List<string>();
            foreach(Train train in _trainlistJson)
            {
                if (!sourceStationList.Contains(train.StartDestination))
                {
                    sourceStationList.Add(train.StartDestination);
                }
                    
            }
            return sourceStationList;
        }

        public List<string> GetAllDestinationStations()
        {
            List<string> destinationStationList = new List<string>();
            foreach (Train train in _trainlistJson)
            {
                if (!destinationStationList.Contains(train.EndDestination))
                    destinationStationList.Add(train.EndDestination);
            }
            return destinationStationList;
        }

        public List<Train> GetTrainsBetweenStations(string source, string destination)
        {
            return _trainlistJson.Where(x => string.Equals(x.EndDestination, destination,StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.StartDestination, source, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<string> GetTrainClass()
        {
            List<string> trainClassList = new List<string>();
            foreach (string trainClass in Enum.GetNames(typeof(TrainClassEnum)))
            {
                trainClassList.Add(trainClass);
            }

            return trainClassList;
        }

        public void DisplayPriceForAllClass(int trainId)
        {
            foreach (Train train in _trainlistJson)
            {
                if (train.TrainId == trainId)
                {

                }

            }
        }

        #region
        //public List<Train> DisplayFromJson()
        //{
        //    Console.WriteLine("Trains on 01/12/2021");
        //    foreach (Train item in trainlistJson)
        //    {
        //        Console.WriteLine("ID: " + item.TrainId);
        //        Console.Write("StartDestination: " + item.StartDestination + "   ");
        //        Console.WriteLine("EndDestination: " + item.EndDestination);
        //        Console.Write("DepartureTime: " + item.DepartureTime.ToShortTimeString() + "   ");
        //        Console.WriteLine("ArrrivalTime: " + item.ArrivalTime.ToShortTimeString());
        //        Console.WriteLine("-------------------------------------------");

        //    }
        //    return trainlistJson;
        //}
        #endregion
        #region using .txt
        //public void InputIntoFile()
        //{
        //    FileStream fs = new FileStream("TrainSchedule2.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //    StreamWriter sw = new StreamWriter(fs);
        //    foreach (Train item in AvailableTrainList)
        //    {
        //        sw.WriteLine(+item.TrainId + "," + item.StartDestination + "," + item.EndDestination + ","
        //            + item.DepartureTime.ToShortTimeString() + "," + item.ArrivalTime.ToShortTimeString() + ","
        //            + item.Distance + "_");

        //    }

        //    sw.Flush();
        //    sw.Close();
        //    fs.Close();
        //}


        //public void DisplayTickets() //reading from file
        //{
        //    FileStream fs = new FileStream("TrainSchedule2.txt", FileMode.Open, FileAccess.Read);
        //    StreamReader sr = new StreamReader(fs);
        //    Console.WriteLine("Train Schedule for 01/12/2021");
        //    sr.BaseStream.Seek(0, SeekOrigin.Begin);
        //    string str = sr.ReadToEnd();

        //    string[] eachStation = str.Split('_');
        //    for (int i = 0; i < eachStation.Length - 1; i++)
        //    {
        //        string[] eachProperty = eachStation[i].Split(',');
        //        Console.WriteLine("ID: " + eachProperty[0]);
        //        Console.WriteLine("Start Destination: " + eachProperty[1]);
        //        Console.WriteLine("End Destination: " + eachProperty[2]);
        //        Console.WriteLine("Departure Time: " + eachProperty[3]);
        //        Console.WriteLine("Arrival Time: " + eachProperty[4]);
        //        Console.WriteLine("--------------------------------");

        //    }

        //    sr.Close();
        //    fs.Close();
        //}
        #endregion
    }
}
