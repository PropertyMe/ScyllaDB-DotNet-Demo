namespace ScyllaDBDemo.Models;

public class Measurement
{
    public Measurement() { }

    public Measurement(Sensor sensor, float value)
    {
        SensorId = sensor.SensorId;
        Timestamp = new(DateTime.UtcNow);
        Value = value;
    }
    
    public Guid SensorId { get; set;}

    public DateTimeOffset Timestamp { get; set; }

    public float Value { get; set; }
}