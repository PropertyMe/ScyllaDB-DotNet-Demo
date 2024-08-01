namespace ScyllaDBDemo.Models;

public class Sensor
{
    public Guid SensorId { get; set; }
    
    public Guid PetId { get; set; }
    
    public SensorType Type { get; set; }

    public static Sensor CreateRandom(Pet pet)
    {
        var random = new Random();
        var randomType = Enum.GetValues<SensorType>()[random.Next(0, 3)];

        return new()
        {
            SensorId = Guid.NewGuid(),
            PetId = pet.PetId,
            Type = randomType
        };
    }

    public double GenerateRandomData()
    {
        var random = new Random();
        return random.NextDouble() * random.Next(1, 4);
    }
}

public enum SensorType
{
    T,
    P,
    L,
    M
}
