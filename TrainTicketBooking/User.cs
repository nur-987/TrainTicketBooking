using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicketBooking
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int NumofTicketsBooked { get; set; }
        public double TotalCost { get; set; }
        public TrainClass TrainClass { get; set; }

    }
}
