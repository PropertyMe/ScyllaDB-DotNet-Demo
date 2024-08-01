namespace ScyllaDBDemo.Models;

public class Pet
{
    public Guid PetId { get; set; } 
    
    public Guid OwnerId { get; set; }
    
    public Guid ChipId { get; set; }
    
    public string Species { get; set; }
    
    public string Gender { get; set; }
    
    public string Address { get; set; }

    public int Age { get; set; }
    
    public float Weight { get; set; }
    
    public string Name { get; set; }
    
    public static Pet CreateRandom(Owner owner)
    {
        var random = new Random();

        return new()
        {
            OwnerId = owner.OwnerId,
            PetId = Guid.NewGuid(),
            ChipId = Guid.NewGuid(),
            Age = random.Next(1, 13),
            Species = $"species_{Guid.NewGuid().ToString()}",
            Gender = $"gender_{Guid.NewGuid().ToString()}",
            Weight = (float)(random.NextDouble() * 10),
            Name = $"pet_name_{Guid.NewGuid().ToString()}",
            Address = $"address_{Guid.NewGuid().ToString()}"
        };
    }
}