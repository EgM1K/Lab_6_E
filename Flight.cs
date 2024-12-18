using System;
using Newtonsoft.Json;
namespace Lab_6
{
    public enum FlightStatus
    {
        OnTime,
        Delayed,
        Cancelled,
        Boarding,
        InFlight
    }
    public class Flight
    {
        [JsonProperty("FlightNumber")]
        public string FlightNumber { get; set; }
        [JsonProperty("Airline")]
        public string Airline { get; set; }
        [JsonProperty("Destination")]
        public string Destination { get; set; }
        [JsonProperty("DepartureTime")]
        public DateTime DepartureTime { get; set; }
        [JsonProperty("ArrivalTime")]
        public DateTime ArrivalTime { get; set; }
        [JsonProperty("Gate")]
        public string Gate { get; set; }
        [JsonProperty("Status")]
        public FlightStatus Status { get; set; }
        [JsonProperty("Duration")]
        public TimeSpan Duration { get; set; }
        [JsonProperty("AircraftType")]
        public string AircraftType { get; set; }
        [JsonProperty("Terminal")]
        public string Terminal { get; set; }
        public override string ToString()
        {
            return $"Flight \n{{\n FlightNumber='{FlightNumber}',\n Airline='{Airline}',\n Destination='{Destination}',\n DepartureTime='{DepartureTime}',\n ArrivalTime='{ArrivalTime}',\n Gate='{Gate}'\n,Status='{Status}',\n Duration='{Duration}',\n AircraftType='{AircraftType}',\n Terminal='{Terminal}'}}";
        }
    }
}