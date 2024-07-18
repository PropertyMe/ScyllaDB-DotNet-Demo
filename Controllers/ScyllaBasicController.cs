using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Services;

namespace ScyllaDBDemo.Controllers
{
    public class ScyllaBasicController : ControllerBase
    {
        private IContactServices _contactServices;

        public ScyllaBasicController(IContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpGet("ScyllaContacts")]
        public async Task<List<Contact>> Get()
        {
            return await _contactServices.GetContactsAsync();
        }
    }
}
