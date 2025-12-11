namespace Semestalka{
    public class Program
    {
    private static string customersPath = "JSONData/customers.json";
    private static string packagesPath = "JSONData/packages.json";
    private static string couriersPath = "JSONData/couriers.json";
    static void Main()
    {
        var customers = LoadData.LoadCustomers(customersPath);
        var packages = LoadData.LoadPackages(packagesPath);
        var couriers = LoadData.LoadCouriers(couriersPath);
        Console.WriteLine($"Loaded C: {customers.Count}, P: {packages.Count}, R: {couriers.Count}");

        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nCurrent day: {DaySimulator.currDay.Date}");
            Console.WriteLine("Choose action: (A)dd package, (C)add customer, (N)ext day, (E)xport & Exit");
            Console.Write("Action: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            switch (input.Trim().ToUpper())
            {
                case "A":
                case "ADD":
                    UserCLI.AddPackageFromConsole(packages, packagesPath);
                    break;

                case "C":
                case "CUSTOMER":
                case "ADD-CUSTOMER":
                    UserCLI.AddCustomerFromConsole(customers, customersPath);
                    break;

                case "N":
                case "NEXT":
                case "ND":
                    // Distribute packages for current day before advancing
                    DeliveryController.DistributePackages(packages, couriers, customers);
                    // Advance to next day
                    DaySimulator.PrepNextDay(packages, customers, couriers);
                    foreach (var c in couriers)
                        c.ResetDay();
                    break;

                case "E":
                case "EXIT":
                case "EXPORT":
                    ExportData.ExportDelivered(packages);
                    ExportData.ExportReturned(packages);
                    ExportData.ExportCourierInfo(couriers);
                    running = false;
                    break;

                default:
                    Console.WriteLine("Unknown action. Use A, C, N or E.");
                    break;
            }
        }
    }
}
}