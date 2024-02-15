namespace Mirea_Avia.Models
{
    public class Ticket
    {
        public string? cityOfDepartue { get; set; }
        public string? cityOfArrival { get; set; }

        public DateTime timeOfDepartue { get; set; }
        public DateTime? timeOfReturn { get; set; }

        public int adultPassengers { get; set; } // >= 12 y.o
        public int kidPassengers { get; set; } // >= 2 && < 12 y.o
        public int babyPassengers { get; set; } // <2 y.o

    }
}
