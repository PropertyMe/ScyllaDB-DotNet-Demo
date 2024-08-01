using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Controllers;

public class Pets(IPetRepository petRepository) : ControllerBase
{
    [HttpGet("measurements")]
    public List<Measurement>  Get(Guid sensorId) => petRepository.GetForSensor(sensorId).ToList();
}