using System;

namespace TrainTicketBooking
{
    public class Ticket
    {
        public Train SelectedTrain { get; set; }
        public TrainClassEnum SelectedClass { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
