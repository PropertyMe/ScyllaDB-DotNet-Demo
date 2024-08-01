namespace ScyllaDBDemo.Models;

public class Owner
{
    public Guid OwnerId { get; set; }

    public string Address { get; set; } = null!;

    public string Name { get; set; } = null!;

    public static Owner CreateRandom()
    {
        return new()
        {
            OwnerId = Guid.NewGuid(),
            Address = $"owner_address_{Guid.NewGuid().ToString()}",
            Name = $"owner_name_{Guid.NewGuid().ToString()}"
        };
    }
}
