using System;

namespace TrainTicketBooking
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public Train SelectedTrain { get; set; }
        public TrainClassEnum SelectedClass { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumOfTickets { get; set; }

    }
}
