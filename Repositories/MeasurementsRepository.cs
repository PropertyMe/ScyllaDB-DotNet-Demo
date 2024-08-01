using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Repositories;

public interface IMeasurementsRepository
{
    IEnumerable<Measurement> GetForSensor(Guid sensorId);
}

public class MeasurementsRepository : IMeasurementsRepository
{
    private const string SensorKeyspace = "carepet";
    private readonly ScyllaConnect _db = new();

    public MeasurementsRepository()
    {
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public IEnumerable<Measurement> GetForSensor(Guid sensorId)
    {
        return _db.Select<Measurement>(
            nameof(Measurement),
            "sensor_id = :sensor_id",
            sensorId
        );
    }
}