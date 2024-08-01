using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Controllers;

public class PetsController(IPetRepository petRepository) : ControllerBase
{
    [HttpGet("pets")]
    public List<Pet> Get(Guid ownerId) => petRepository.GetForOwner(ownerId).ToList();
}