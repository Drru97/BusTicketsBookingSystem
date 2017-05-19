namespace BookingSystem.Entities
{
    public class Traffic
    {
        public int Id { get; set; }

        public int JourneyId { get; set; }

        public int TicketId { get; set; }

        public virtual Journey Journey { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
