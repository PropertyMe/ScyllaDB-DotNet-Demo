using Cassandra.Mapping;
using ScyllaDBDemo.Models;

public class ModelConfiguration : Mappings
{
    public ModelConfiguration()
    {
        MappingConfiguration.Global.Define(
            new Map<Measurement>()
                .TableName("measurement")
                .PartitionKey(m => m.SensorId)
                .Column(m => m.SensorId, cm => cm.WithName("sensor_id"))
                .Column(m => m.Timestamp, cm => cm.WithName("ts"))
        );
    }
}