namespace ScyllaDBDemo.Models;

public class Measurement(Sensor sensor, float value)
{
    public Guid SensorId { get; set;} = sensor.PetId;

    public DateTimeOffset Timestamp { get; set; } = new(DateTime.UtcNow);

    public float Value { get; set; } = value;
}