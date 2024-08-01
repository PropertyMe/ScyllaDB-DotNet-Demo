using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Controllers;

public class OwnersController(IOwnersRepository ownersRepository) : ControllerBase
{
    [HttpGet("owners")]
    public List<Owner> Get() => ownersRepository.GetAll().ToList();
}