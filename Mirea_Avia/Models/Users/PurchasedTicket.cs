using Mirea_Avia.Models.Search;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mirea_Avia.Models.Users
{
    public sealed class PurchasedTicket
    {
        [Key]
        public int ticket_id { get; set; }

        [ForeignKey("city_from")]
        public City FK_Ticket_City_From { get; set; }

        [ForeignKey("city_to")]
        public City FK_Ticket_City_To { get; set; }

        [ForeignKey("user")]
        public User FK_Ticket_User { get; set; }

        public double price { get; set; }

        public DateTime date_from { get; set; }


        public int user { get; set; }
        public int city_from { get; set; }
        public int city_to { get; set; }

    }
}
