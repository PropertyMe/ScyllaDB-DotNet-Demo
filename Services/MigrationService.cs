using Newtonsoft.Json;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services;

public interface IMigrationService
{
    void Execute();

    void Seed();
}

public class MigrationService : IMigrationService
{
    private readonly ScyllaConnect _db = new();
    
    public void Execute()
    {
        Console.WriteLine("Beginning migrations...");

        try {
            ExecuteMigrations();
            Console.WriteLine("Successfully ran migrations");
        } catch (Exception ex) {
            Console.WriteLine("Failed to execute migrations");
            Console.WriteLine(ex.Message);
        }
    }

    private void ExecuteMigrations()
    {
        ExecuteMigration("create_local_dev_keyspace.cql");
        ExecuteMigration("alter_local_dev_keyspace.cql");
        ExecuteMigration("create_contact.cql");
        ExecuteMigration("create_carepet_keyspace.cql");
        ExecuteMigration("create_owner_table.cql");
        ExecuteMigration("create_pet_table.cql");
        ExecuteMigration("create_measurement_table.cql");
        ExecuteMigration("create_sensor_table.cql");
        ExecuteMigration("create_sensoravg_table.cql");
    }

    private void ExecuteMigration(string migrationFn)
    {
        try {
            var createKeyspace = ReadFile($"Migrations/{migrationFn}");
            _db.Session.Execute(createKeyspace);
            
            Console.WriteLine("-----");
            Console.WriteLine($"Applied migration {migrationFn}");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Migration {migrationFn} failed with error, {ex.Message}");
        }
    }

    public void Seed()
    {
        Console.WriteLine("Beginning to seed data...");
        
        try {
            var json = ReadFile("SeedData/contacts.json");
            var seedData = JsonConvert.DeserializeObject<List<Contact>>(json);

            // note that this is hardcoded to the below keyspace for now
            _db.Session.ChangeKeyspace("local_dev");
            foreach (var contact in seedData) {
                _db.Insert<Contact>(
                    ["id", "address", "email", "username"],
                    [Guid.NewGuid(), contact.Address, contact.Email, contact.Username]
                );
            }

            Console.WriteLine("Successfully finished seeding data");
        } catch (Exception ex) {
            Console.WriteLine("Failed to seed data");
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