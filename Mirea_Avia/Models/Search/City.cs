using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mirea_Avia.Models.Search
{
    public sealed class City
    {
        [Key]
        public int city_id { get; set; }

        [ForeignKey("country")]
        public Country FK_Country_City { get; set; }
        public string city_name { get; set; }
        public string city_code { get; set; }
    }
}
