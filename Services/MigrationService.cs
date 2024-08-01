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

        try {
            ExecuteMigrations();
            Console.WriteLine("Successfully ran migrations");
        } catch (Exception ex) {
            Console.WriteLine("Failed to execute migrations");
            Console.WriteLine(ex.Message);
        }
    }

    private static void ExecuteMigrations()
    {
        var db = new ScyllaConnect();
        ExecuteMigration(db, "create_local_dev_keyspace.cql");
        ExecuteMigration(db, "alter_local_dev_keyspace.cql");
        ExecuteMigration(db, "create_contact.cql");
        ExecuteMigration(db, "create_carepet_keyspace.cql");
        ExecuteMigration(db, "create_owner_table.cql");
        ExecuteMigration(db, "create_pet_table.cql");
        ExecuteMigration(db, "create_measurement_table.cql");
        ExecuteMigration(db, "create_sensor_table.cql");
        ExecuteMigration(db, "create_sensoravg_table.cql");
    }

    private static void ExecuteMigration(ScyllaConnect db, string migrationFn)
    {
        try {
            var createKeyspace = ReadFile($"Migrations/{migrationFn}");
            db.Session.Execute(createKeyspace);
            
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