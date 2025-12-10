class Program
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

        for (int i = 1; i < 5; i++)
        {
            Console.WriteLine($"****DAY {i}****");

            DeliveryController.DistributePackages(packages, couriers, customers);
            
            foreach(var customer in customers) 
                DaySimulator.NextDay(packages, customer);

            foreach (var c in couriers)
                c.ResetDay();
        }


        ExportData.ExportDelivered(packages);

        ExportData.ExportReturned(packages);

        ExportData.ExportCourierInfo(couriers);
    }
}