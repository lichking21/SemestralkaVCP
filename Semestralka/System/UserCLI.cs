public static class UserCLI
{
    public static void AddPackageFromConsole(List<Package> packages, string packagesPath)
    {
        int nextId = packages.Any() ? packages.Max(p => p.ID) + 1 : 1;
        Console.WriteLine($"Adding new package (ID {nextId})");

        Console.Write("Address: ");
        var address = Console.ReadLine() ?? string.Empty;

        Console.Write("Customer name: ");
        var customer = Console.ReadLine() ?? string.Empty;

        Console.Write("Weight (int): ");
        if (!int.TryParse(Console.ReadLine(), out int weight)) weight = 1;

        Console.Write("Size (Small, Medium, Big): ");
        var sizeStr = Console.ReadLine() ?? "Small";
        if (!Enum.TryParse<PackageSize>(sizeStr, true, out var size)) size = PackageSize.Small;

        Console.Write("COD (int): ");
        if (!int.TryParse(Console.ReadLine(), out int cod)) cod = 0;

        var p = new Package {
            ID = nextId,
            Address = address,
            CustomerName = customer,
            Weight = weight,
            Size = size,
            COD = cod,
            Status = PackageStatus.Pending,
            DeliveryAttempts = 0
        };

        packages.Add(p);
        SavePackagesToJson(packages, packagesPath);
        Console.WriteLine($"Package {p.ID} added and saved.");
    }
    public static void AddCustomerFromConsole(List<Customer> customers, string customersPath)
    {
        Console.WriteLine("Adding new customer");
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Address: ");
        var address = Console.ReadLine() ?? string.Empty;

        var c = new Customer { Name = name, Address = address, IsAtHome = false };
        customers.Add(c);
        SaveCustomersToJson(customers, customersPath);
        Console.WriteLine($"Customer {c.Name} at {c.Address} added and saved.");
    }
    public static void SavePackagesToJson(List<Package> packages, string filePath)
    {
        try
        {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize(packages, options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving packages: {ex.Message}");
        }
    }

    public static void SaveCustomersToJson(List<Customer> customers, string filePath)
    {
        try
        {
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize(customers, options);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving customers: {ex.Message}");
        }
    }
}