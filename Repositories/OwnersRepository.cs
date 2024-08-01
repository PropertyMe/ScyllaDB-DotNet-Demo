using ScyllaDBDemo.Database;
using ScyllaDBDemo.Models;

namespace ScyllaDBDemo.Repositories;

public interface IOwnersRepository
{
    IEnumerable<Owner> GetAll();
}

public class OwnersRepository : IOwnersRepository
{
    private const string SensorKeyspace = "carepet";
    private readonly IScyllaDb _db;

    public OwnersRepository(IScyllaDb scyllaDb)
    {
        _db = scyllaDb;
        _db.Session.ChangeKeyspace(SensorKeyspace);
    }

    public IEnumerable<Owner> GetAll() => _db.Select<Owner>();
}