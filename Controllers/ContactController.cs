using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Services;

namespace ScyllaDBDemo.Controllers;

[ApiController]
[Route("api/contacts")]
public class ContactController(IContactServices contactServices) : ControllerBase
{
    [HttpGet]
    public Task<List<Contact>> Get() => contactServices.GetContactsAsync();
}
