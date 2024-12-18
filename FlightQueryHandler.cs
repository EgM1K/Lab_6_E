using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_6
{
    public class FlightQueryHandler
    {
        private FlightInformationSystem flightSystem;
        public FlightQueryHandler(FlightInformationSystem flightSystem)
        {
            this.flightSystem = flightSystem;
        }
        public List<Flight> FindFlightsByNumber(string flightNumber)
        {
            var flights = flightSystem.GetFlights();

            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }
            List<Flight> matchingFlights = new List<Flight>();

            foreach (var flight in flights)
            {
                if (flight.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase))
                {
                    matchingFlights.Add(flight);
                }
            }
            return matchingFlights;
        }
        public List<Flight> FindFlightsByStatus(FlightStatus status)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }
            return flights.Where(f => f.Status == status).ToList();
        }
        public List<Flight> FindFlightsByAirline(string airline)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }
            return flights.Where(f => f.Airline.Equals(airline, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Flight> FindFlightsByDestination(string destination)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }
            return flights.Where(f => f.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Flight> FindFlightsByDepartureTime(DateTime departureTime)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }
            return flights.Where(f => f.DepartureTime == departureTime).ToList();
        }
        public List<Flight> FindFlightsByDate(DateTime date)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }

            var result = new List<Flight>();
            foreach (var flight in flights)
            {
                if (flight.DepartureTime.Date == date.Date || flight.ArrivalTime.Date == date.Date)
                {
                    result.Add(flight);
                }
            }
            return result;
        }

        public List<Flight> FindFlightsInTimeRange(DateTime startTime, DateTime endTime)
        {
            var flights = flightSystem.GetFlights();
            if (flights == null)
            {
                Console.WriteLine("Дані про рейси недоступні.");
                return new List<Flight>();
            }

            var result = new List<Flight>();
            foreach (var flight in flights)
            {
                if ((flight.DepartureTime >= startTime && flight.DepartureTime <= endTime) ||
                    (flight.ArrivalTime >= startTime && flight.ArrivalTime <= endTime))
                {
                    result.Add(flight);
                }
            }
            return result;
        }

        public void DisplayFlights(List<Flight> flights)
        {
            if (flights == null || !flights.Any())
            {
                Console.WriteLine("Відповідних рейсів не знайдено.");
                return;
            }
            foreach (var flight in flights)
            {
                Console.WriteLine(flight.ToString());
            }
        }
    }
}
