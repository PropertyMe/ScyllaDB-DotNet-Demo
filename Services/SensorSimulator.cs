using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Services;

public interface ISensorSimulator
{
    Task RunSimulation(TimeSpan bufferInterval, TimeSpan measurementWindow);
}

public class SensorSimulator : ISensorSimulator
{
    private const string SensorKeyspace = "carepet";
    private readonly ScyllaConnect _db = new();

    public SensorSimulator()
    {
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public async Task RunSimulation(TimeSpan bufferInterval, TimeSpan measurementWindow)
    {
        var sensors = BootstrapEntities();
        await ExecuteMeasurements(sensors, bufferInterval, measurementWindow);
    }

    private async Task ExecuteMeasurements(Sensor[] sensors, TimeSpan bufferInterval, TimeSpan measurementWindow)
    {
        Console.WriteLine($"Generating fake sensor data every {measurementWindow} | buffered for batch insertion every {bufferInterval}");

        var lastMeasurement = DateTime.UtcNow;

        // event loop
        while (true)
        {
            var buffer = new List<Measurement>();

            // buffer loop
            while (DateTime.UtcNow - lastMeasurement <  bufferInterval)
            {
                await Task.Delay(measurementWindow);
                buffer.AddRange(sensors
                    .Select(s =>
                    {
                        var m = new Measurement(s, s.GenerateRandomData());
                        Console.WriteLine($"{s.Type} sensor #{s.SensorId} measured new value: {m.Value} | UTC Time: {m.Timestamp.DateTime.ToUniversalTime()}");
                        return m;
                    })
                    .ToArray()
                );
            }
            
            lastMeasurement = DateTime.UtcNow;
            Console.WriteLine("Pushing buffer to database");

            _db.BatchInsert(
                nameof(Measurement),
                ["sensor_id", "ts", "value"],
                buffer.Select(m => new object[] { m.SensorId, m.Timestamp, m.Value}).ToArray()
            );
        }

        // this is expected to run as an inf. event loop
        // ReSharper disable once FunctionNeverReturns
    }

    private Sensor[] BootstrapEntities()
    {
        Console.WriteLine("Generating fake sensor data...");

        var (owner, pet, sensors) = CreateRandomData();

        _db.Insert(
            nameof(Owner),
            ["owner_id", "address", "name"],
            [owner.OwnerId, owner.Address, owner.Name]
        );
        
        Console.WriteLine($"New owner #{owner.OwnerId}");
        
        _db.Insert(
            nameof(Pet),
            ["pet_id", "owner_id", "age", "weight", "name"],
            [pet.PetId, pet.OwnerId, pet.Age, pet.Weight, pet.Name]
        );
        
        Console.WriteLine($"New pet #{pet.PetId}");

        foreach (var sensor in sensors)
        {
            _db.Insert(
                nameof(Sensor),
                ["sensor_id", "pet_id", "type"],
                // ignoring the null case for this example
                [sensor.SensorId, sensor.PetId, Enum.GetName(sensor.Type)!]
            );

            Console.WriteLine($"New sensor #{sensor.SensorId}");
        }

        return sensors;
    }

    private static (Owner, Pet, Sensor[]) CreateRandomData()
    {
        var owner = Owner.CreateRandom();
        var pet = Pet.CreateRandom(owner);

        var random = new Random();
        var sensors = new int[random.Next(1, 4)].Select(_ => Sensor.CreateRandom(pet)).ToArray();

        return (owner, pet, sensors);
    }
}