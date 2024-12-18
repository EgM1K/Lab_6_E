using System;
using System.IO;
using Newtonsoft.Json;

namespace Lab_6
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            FlightInformationSystem flightSystem = new FlightInformationSystem("C:\\Users\\egorm\\source\\repos\\Lab_6\\flights_data.json");
            FlightQueryHandler queryHandler = new FlightQueryHandler(flightSystem);
            string reportDirectory = @"C:\Users\egorm\source\repos\Lab_6\reports";
            Directory.CreateDirectory(reportDirectory);

            while (true)
            {
                Console.WriteLine("\nВиберіть тип: ");
                Console.WriteLine("1. Номер рейсу");
                Console.WriteLine("2. Статус рейсу");
                Console.WriteLine("3. Авіалінія");
                Console.WriteLine("4. Місце призначення");
                Console.WriteLine("5. Час відправлення");
                Console.WriteLine("6. Проміжок часу");
                Console.WriteLine("7. Дата");
                Console.WriteLine("0. Вихід");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 7)
                {
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                    continue;
                }

                if (choice == 0) break;

                List<Flight> result = null;
                string reportType = "";

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введіть номер рейсу (П:BA836): ");
                        string flightNumber = Console.ReadLine();
                        result = queryHandler.FindFlightsByNumber(flightNumber);
                        reportType = $"Flight Number: {flightNumber}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 2:
                        Console.WriteLine("Введіть статус рейсу (OnTime, Delayed, Cancelled, Boarding, InFlight):");
                        FlightStatus status = (FlightStatus)Enum.Parse(typeof(FlightStatus), Console.ReadLine());
                        result = queryHandler.FindFlightsByStatus(status);
                        reportType = $"Flight Status: {status}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 3:
                        Console.WriteLine("Введіть авіалінію (П:MAU):");
                        string airline = Console.ReadLine();
                        result = queryHandler.FindFlightsByAirline(airline);
                        reportType = $"Airline: {airline}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 4:
                        Console.WriteLine("Введіть місце призначення (П:Kyiv):");
                        string destination = Console.ReadLine();
                        result = queryHandler.FindFlightsByDestination(destination);
                        reportType = $"Destination: {destination}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 5:
                        Console.WriteLine("Введіть час відправлення (П:2023-06-14T17:03:13):");
                        DateTime departureTime = DateTime.Parse(Console.ReadLine());
                        result = queryHandler.FindFlightsByDepartureTime(departureTime);
                        reportType = $"Departure Time: {departureTime}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 6:
                        Console.WriteLine("Введіть початкову дату (П:2023-01-27T07:43:15):");
                        DateTime startTime = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Введіть кінцеву дату (П:2023-01-27T16:03:15):");
                        DateTime endTime = DateTime.Parse(Console.ReadLine());
                        result = queryHandler.FindFlightsInTimeRange(startTime, endTime);
                        reportType = $"Time Range: {startTime} - {endTime}";
                        queryHandler.DisplayFlights(result);
                        break;

                    case 7:
                        Console.WriteLine("Введіть дату (П:2023-07-23):");
                        DateTime date = DateTime.Parse(Console.ReadLine());
                        result = queryHandler.FindFlightsByDate(date);
                        reportType = $"Date: {date}";
                        queryHandler.DisplayFlights(result);
                        break;
                }

                if (result != null && result.Count > 0)
                {
                    int reportCount = Directory.GetFiles(reportDirectory).Length + 1;
                    string reportPath = Path.Combine(reportDirectory, $"report_{reportCount}.json");
                    Report report = new Report
                    {
                        ReportType = reportType,
                        Result = result,
                        GeneratedAt = DateTime.Now
                    };
                    ReportGenerator.GenerateReport(report, reportPath);
                    Console.WriteLine($"Репорт збережено: {reportPath}");
                }
                else
                {
                    Console.WriteLine("Рейси не знайдено.");
                }

                Console.WriteLine("\nДядя шо хочеш?");
                Console.WriteLine("1. Повернутися до меню");
                Console.WriteLine("2. Завершити");
                int actionChoice;
                if (int.TryParse(Console.ReadLine(), out actionChoice) && actionChoice == 2)
                {
                    break;
                }
            }

            Console.WriteLine("Програму завершено.");
        }
    }

    public class Report
    {
        public string ReportType { get; set; }
        public object Result { get; set; }
        public DateTime GeneratedAt { get; set; }
    }

    public static class ReportGenerator
    {
        public static void GenerateReport(Report data, string outputFilePath)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(outputFilePath, json);
        }
    }
}
