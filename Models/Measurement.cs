namespace ScyllaDBDemo.Models;

public class Measurement(Sensor sensor, double value)
{
    public Guid SensorId { get; set;} = sensor.PetId;

    public DateTimeOffset Timestamp { get; set; } = new(DateTime.UtcNow);

    public double Value { get; set; } = value;
}