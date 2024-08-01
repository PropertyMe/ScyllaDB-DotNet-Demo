using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services;

public interface IMigrationService
{
    void Execute();

    void Seed();
}

public class MigrationService : IMigrationService
{
    public void Execute()
    {
        Console.WriteLine("Beginning migrations...");
        var db = new ScyllaConnect();

        try
        {
            var createKeyspace = ReadFile("Migrations/create_keyspace.cql");
            db.Session.Execute(createKeyspace);

            var createContact = ReadFile("Migrations/create_contact.cql");
            db.Session.Execute(createContact);

            Console.WriteLine("Successfully ran migrations ✅");
        } catch (Exception ex) {
            Console.WriteLine("Failed to execute migrations ❌");
            // note that this spits out a very simple error with a line number form the CQL
            // it would be nice to enrich this with whichever migration failed
            Console.WriteLine(ex.Message);
        }
    }

    public void Seed()
    {
        Console.WriteLine("Beginning to seed data...");
        var db = new ScyllaConnect();
        
        try
        {
            var seedData = ReadFile("Migrations/seed_simple_contacts.cql");
            db.Session.Execute(seedData);

            Console.WriteLine("Successfully finished seeding data 🌱");
        } catch (Exception ex) {
            Console.WriteLine("Failed to seed data ❌");
            Console.WriteLine(ex.Message);
        }
    }

    private static string ReadFile(string fn)
    {
        var rv = "";

        try
        {
            using StreamReader reader = new(fn);
            rv = reader.ReadToEnd();
        }
        catch (IOException ex)
        {
            Console.WriteLine("Couldn't read the given file");
            Console.WriteLine(ex.Message);
        }

        return rv;
    }
}