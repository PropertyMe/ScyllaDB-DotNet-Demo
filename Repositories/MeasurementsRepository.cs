using Cassandra;
using ScyllaDBDemo.Database;
using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Repositories;

public interface IMeasurementsRepository
{
    IEnumerable<Measurement> GetForSensor(Guid sensorId);

    /// <summary>
    /// Gets any existing averages for the sensor over the 24hr period of the given date.
    /// Averages are hourly, so 24 results should be expected.
    /// If less than 24 averages exist, we'll look up any measurements in the same day and determine the remaining
    /// possible statistics. 
    /// </summary>
    /// <param name="sensorId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    IEnumerable<SensorAverage> GetOrCreateAveragesForSensor(Guid sensorId, DateTime date);
}

public class MeasurementsRepository : IMeasurementsRepository
{
    private const string SensorKeyspace = "carepet";
    private readonly IScyllaDb _db;

    public MeasurementsRepository(IScyllaDb scyllaDb)
    {
        _db = scyllaDb;
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public IEnumerable<Measurement> GetForSensor(Guid sensorId)
    {
        return _db.Select<Measurement>(
            "sensor_id = :sensor_id",
            sensorId
        );
    }

    public IEnumerable<SensorAverage> GetOrCreateAveragesForSensor(Guid sensorId, DateTime date)
    {
        var averages = _db.Select<SensorAverage>(
            "sensor_id = :sensor_id and date = :date", 
            [sensorId, new LocalDate(date.Year, date.Month, date.Day)]
        ).ToList();
        
        // we average per day, ie. 24hrs
        if (averages.Count >= 24) return averages;
        
        averages = AggregateMeasurements(sensorId, date, averages);
            
        // persist the newly materialised averages
        _db.BatchInsert<SensorAverage>(
            ["sensor_id", "date", "hour", "value"],
            averages.Select(a => new object[] { a.SensorId, a.Date, a.Hour, a.Value }).ToArray()
        );

        return averages;
    }

    private List<SensorAverage> AggregateMeasurements(Guid id, DateTime date, List<SensorAverage> existingAverages)
    {
        var startOfWindow = new DateTime(date.Year, date.Month, date.Day,
            // however many stat's we have already determines what hour we start at
            existingAverages.Count, 0, 0
        );

        var endOfWindow = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        var measurements = _db.Select<Measurement>(
            "sensor_id = :sensor_id and ts >= ? and ts <= ?",
            [id, startOfWindow, endOfWindow]
        ).ToList();

        var averages = measurements
            .GroupBy(m => m.Timestamp.Hour)
            .Select(g => new SensorAverage()
            {
                Date = new(date.Date.Year, date.Date.Month, date.Date.Day),
                Hour = g.Key,
                SensorId = id,
                Value = g.Sum(m => m.Value) / g.Count()
            }).ToList();

        return averages;
    }
}