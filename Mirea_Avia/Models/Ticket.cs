using Newtonsoft.Json;


namespace Mirea_Avia.Models
{
    public class Ticket
    {
        [JsonProperty(PropertyName = "origin")]
        public string? cityOfDepartue { get; set; }

        [JsonProperty(PropertyName = "destination")]
        public string? cityOfArrival { get; set; }

        [JsonProperty(PropertyName = "departure_at")]
        public string timeOfDepartue { get; set; }

        [JsonProperty(PropertyName = "airline")]
        public string airline { get; set; }

        [JsonProperty(PropertyName = "price")]
        public float price { get; set; }

    }
}
