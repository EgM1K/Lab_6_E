using System.Collections.Generic;
using Newtonsoft.Json;
namespace Lab_6
{

    public class FlightData
    {
        [JsonProperty("flights")]
        public List<Flight> Flights { get; set; }
    }
}