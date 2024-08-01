using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Repositories;

public interface IOwnersRepository
{
    IEnumerable<Owner> GetAll();
}

public class OwnersRepository : IOwnersRepository
{
    private const string SensorKeyspace = "carepet";
    private readonly ScyllaConnect _db = new();

    public OwnersRepository()
    {
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public IEnumerable<Owner> GetAll() => _db.Select<Owner>(nameof(Owner));
}