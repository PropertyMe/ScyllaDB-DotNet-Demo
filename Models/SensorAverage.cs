using Cassandra;

namespace ScyllaDBDemo.Models;

public class SensorAverage
{
    public SensorAverage() { }
    
    public Guid SensorId { get; set; }
    
    // Cassandra native type, representing YYYY-MM-DD
    public LocalDate Date { get; set; }
    
    public int Hour { get; set; }
    
    public float Value { get; set; }
}