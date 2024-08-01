using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Controllers;

[ApiController]
[Route("api/owners")]
public class OwnersController(IOwnersRepository ownersRepository, IPetRepository petRepository) : ControllerBase
{
    [HttpGet]
    public List<Owner> Get() => ownersRepository.GetAll().ToList();
    
    [HttpGet("{ownerId:guid}/pets")]
    public List<Pet> Get(Guid ownerId) => petRepository.GetForOwner(ownerId).ToList();
}