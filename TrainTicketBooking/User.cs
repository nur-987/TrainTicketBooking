using System.Collections.Generic;

namespace TrainTicketBooking
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Ticket> TicketHistory { get; set; }

    }
}
