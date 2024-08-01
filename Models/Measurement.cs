namespace ScyllaDBDemo.Models;

public class Measurement(Sensor sensor, double value)
{
    public Guid SensorId { get; set;} = sensor.PetId;

    public long Ticks { get; set; } = DateTime.UtcNow.Ticks;

    public double Value { get; set; } = value;
}