using Microsoft.Extensions.Logging;
using ContactCatalog;


using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Program startar");

var service = new ContactService(logger);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Contact Catalog ===");
    Console.WriteLine("1) Lägg till kontakt");
    Console.WriteLine("2) Lista alla");
    Console.WriteLine("3) Sök på namn");
    Console.WriteLine("4) Filtrera på tagg");
    Console.WriteLine("0) Avsluta");
    Console.Write("> ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": AddContact(service); break;
        case "2": ListContacts(service.GetAll()); break;
        case "3":
            Console.Write("Sökterm: ");
            var term = Console.ReadLine();
            ListContacts(service.SearchByName(term ?? ""));
            break;
        case "4":
            Console.Write("Tagg: ");
            var tag = Console.ReadLine();
            ListContacts(service.FilterByTag(tag ?? ""));
            break;
        case "0": return;
        default: Console.WriteLine("Ogiltigt val."); break;
    }

    Console.WriteLine("\nTryck valfri tangent för att fortsätta...");
    Console.ReadKey();
}

static void AddContact(ContactService service)
{
    try
    {
        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Namn: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("E-post: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Taggar (komma-separerade): ");
        var tagInput = Console.ReadLine() ?? "";
        var tags = tagInput.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

        var contact = new Contact { Id = id, Name = name, Email = email, Tags = tags };
        service.AddContact(contact);

        Console.WriteLine("[Lagd!] Kontakt tillagd.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fel: {ex.Message}");
    }
}

static void ListContacts(IEnumerable<Contact> contacts)
{
    foreach (var c in contacts)
    {
        Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(',', c.Tags)}]");
    }
}
