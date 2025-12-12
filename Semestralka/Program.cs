using Semestalka;

namespace Semestalka{
    public class Program
    {
    private static string customersPath = "JSONData/customers.json";
    private static string packagesPath = "JSONData/packages.json";
    private static string couriersPath = "JSONData/couriers.json";
    static void Main()
    {

        LoadData simData = new LoadData(customersPath, packagesPath, couriersPath); 
        Console.WriteLine($"Loaded C: {simData.customers.Count}, P: {simData.packages.Count}, R: {simData.couriers.Count}");
        UserCLI.execute(simData);
    }
    }
}