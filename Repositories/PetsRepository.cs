using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Repositories;

public interface IPetRepository
{
    IEnumerable<Pet> GetForOwner(Guid ownerId);
}

public class PetRepository : IPetRepository
{
    private const string SensorKeyspace = "carepet";
    private readonly ScyllaConnect _db = new();

    public PetRepository()
    {
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public IEnumerable<Pet> GetForOwner(Guid ownerId)
    {
        return _db.Select<Pet>(
            nameof(Pet),
            "sensor_id = :sensor_id",
            ownerId
        );
    }
}