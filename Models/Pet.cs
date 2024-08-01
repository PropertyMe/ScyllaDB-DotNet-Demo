namespace ScyllaDBDemo.Models;

public class Pet
{
    public Guid PetId { get; set; } 
    
    public Guid OwnerId { get; set; }

    public int Age { get; set; }
    
    public double Weight { get; set; }
    
    public string Name { get; set; }
    
    public static Pet CreateRandom(Owner owner)
    {
        var random = new Random();

        return new()
        {
            OwnerId = owner.OwnerId,
            PetId = Guid.NewGuid(),
            Age = random.Next(1, 13),
            Weight = random.NextDouble() * 10,
            Name = $"pet_name_{Guid.NewGuid().ToString()}"
        };
    }
}