using Newtonsoft.Json;
using System;
using System.IO;
namespace Lab_6
{
    public class FlightInformationSystem
    {
        private FlightData flightData;
        public List<Flight> Flights { get; private set; }

        public FlightInformationSystem(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                flightData = JsonConvert.DeserializeObject<FlightData>(json);
                if (flightData?.Flights == null || !flightData.Flights.Any())
                {
                    Console.WriteLine("No flight data found.");
                }
                else
                {
                    Console.WriteLine("Flight data loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading flight data: " + ex.Message);
            }
        }
        public List<Flight> GetFlights()
        {
            return flightData?.Flights;
        }
        private void LoadFlightsFromJson(string jsonFilePath)
        {
            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                Flights = JsonConvert.DeserializeObject<List<Flight>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flight data: {ex.Message}");
                Flights = new List<Flight>();
            }
        }
        public void DisplayFlights()
        {
            if (Flights != null && Flights.Count > 0)
            {
                foreach (var flight in Flights)
                {
                    Console.WriteLine(flight.ToString());
                }
            }
            else
            {
                Console.WriteLine("No flights available.");
            }
        }
    }
}