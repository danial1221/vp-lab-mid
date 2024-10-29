
using System;
using System.Collections.Generic;

public class StudentClub
{
    private double budget;

    public void FundSociety(double amount)
    {
        budget += amount;
        Console.WriteLine($"Funds allocated: ${amount}. Total budget: ${budget}");
    }

    public double GetBudget()
    {
        return budget;
    }

    public void DisplaySocietyInfo(List<Society> societies)
    {
        foreach (var society in societies)
        {
            Console.WriteLine($"Society Name: {society.Name}, Contact: {society.Contact}, Funding: ${society.GetFunding()}");
        }
    }
}

public class Society : StudentClub
{
    public string Name { get; private set; }
    public string Contact { get; private set; }

    private List<string> events = new List<string>();
    private double funding;

    public Society(string name, string contact)
    {
        Name = name;
        Contact = contact;
    }

    public void AddActivity(string activity)
    {
        events.Add(activity);
        Console.WriteLine($"Activity '{activity}' added to society '{Name}'.");
    }

    public void ListEvents()
    {
        Console.WriteLine($"Events for society '{Name}':");
        foreach (var activity in events)
        {
            Console.WriteLine($"- {activity}");
        }
    }

    public void AllocateFunding(double amount)
    {
        funding += amount;
    }

    public double GetFunding()
    {
        return funding;
    }
}

public class FundedSociety : Society
{
    public FundedSociety(string name, string contact) : base(name, contact) { }
}

public class NonFundedSociety : Society
{
    public NonFundedSociety(string name, string contact) : base(name, contact) { }
}

public class ClubRole 
{
    public string Name { get; private set; }
    public string Role { get; private set; }
    public string ContactInfo { get; private set; }

    public ClubRole(string name, string role, string contactInfo)
    {
        Name = name;
        Role = role;
        ContactInfo = contactInfo;
    }
}

public class Program
{
    private static List<Society> societies = new List<Society>();

    public static void Main(string[] args)
    {
        // Initialize preset societies with allocated funds
        societies.Add(new FundedSociety("Techbit Society", "techbit@example.com") { });
        societies[0].AllocateFunding(600); // Techbit Society with $600

        societies.Add(new FundedSociety("Literary Society", "literary@example.com") { });
        societies[1].AllocateFunding(500); // Literary Society with $500

        societies.Add(new FundedSociety("Sports Society", "sports@example.com") { });
        societies[2].AllocateFunding(500); // Sports Society with $500

        while (true)
        {
            Console.WriteLine("\n    Student Club Management System  ");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1. Register a new society");
            Console.WriteLine("2. Allocate funding to a society");
            Console.WriteLine("3. Register an event for a society");
            Console.WriteLine("4. Display society funding info");
            Console.WriteLine("5. Display events for a society");
            Console.WriteLine("6. Exit");
            Console.WriteLine("---------------------------------------");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n");
                    RegisterSociety();
                    break;

                case "2":
                    Console.WriteLine("/n");
                    AllocateFunding();
                    break;

                case "3":
                    Console.WriteLine("\n");
                    RegisterEvent();
                    break;

                case "4":
                    Console.WriteLine("\n");
                    DisplayFundingInfo();
                    break;

                case "5":
                    Console.WriteLine("\n");
                    DisplayEvents();
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid input .....");
                    break;
            }
        }
    }

    private static void RegisterSociety()
    {
        Console.Write("Enter society name: ");
        string name = Console.ReadLine();

        Console.Write("Purpose of This Society");

        String Purpose= Console.ReadLine();
        Console.Write("Enter contact info: ");

        string contact = Console.ReadLine();

        Console.WriteLine("Is this a funded society? (y/n)");
        string isFunded = Console.ReadLine().ToLower();

        Society society = isFunded == "y" ? new FundedSociety(name, contact) : new NonFundedSociety(name, contact);
        societies.Add(society);
        Console.WriteLine($"Society '{name}' registered successfully.");
    }

    private static void AllocateFunding()
    {
        Console.Write("Enter society name for funding: ");
        string name = Console.ReadLine();
        Society society = societies.Find(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (society != null)
        {
            Console.Write("Enter funding amount: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                society.AllocateFunding(amount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
        else
        {
            Console.WriteLine(" Society not found.");
        }
    }

    private static void RegisterEvent()
    {
        Console.Write("Enter society name for event registration: ");
        string name = Console.ReadLine();
        Society society = societies.Find(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (society != null)
        {
            Console.Write("Enter event name: ");
            string eventName = Console.ReadLine();
            society.AddActivity(eventName);
        }
        else
        {
            Console.WriteLine("Society not found.");
        }
    }

    private static void DisplayFundingInfo()
    {
        foreach (var society in societies)
        {
            Console.WriteLine($"Society: {society.Name}, Funding: ${society.GetFunding()}");
        }
    }

    private static void DisplayEvents()
    {
        Console.Write("Enter society to show  event: ");
        string name = Console.ReadLine();
        Society society = societies.Find(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (society != null)
        {
            society.ListEvents();
        }
    }
}

