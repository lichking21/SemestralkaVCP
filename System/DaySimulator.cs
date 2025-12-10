public class DaySimulator
{
    public static DateTime currDay = DateTime.Today;

    public static void PrepNextDay(List<Package> packages, List<Customer> customers, List<Courier> couriers)
    {
        foreach(var customer in customers)
        {
            Random rnd = new Random();
            customer.SetHomeStatus(rnd.Next(2) == 0);
        }
        
        currDay = currDay.AddDays(1);
        foreach(var p in packages.Where(p => p.Status == PackageStatus.PickUpWaiting))
        {
            if (currDay > p.PickUpDeadline)
            {
                p.Status = PackageStatus.Returned;
                // find the courier who had this package today and increment their returned count
                var courier = couriers.FirstOrDefault(c => c.DailyPackages.Contains(p));
                if (courier != null)
                {
                    courier.ReturnedCount++;
                }
                Console.WriteLine($"(RETURNED) Package{p.ID} status: {p.Status}");
            }
        }

    } 
}