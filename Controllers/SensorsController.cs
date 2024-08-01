using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;
using ScyllaDBDemo.Services;

namespace ScyllaDBDemo.Controllers;

[ApiController]
[Route("api/sensors")]
public class SensorsController(ISensorSimulator sensorSimulator, IMeasurementsRepository measurementsRepository) : ControllerBase
{
    [HttpGet("simulate")]
    public Task Get(TimeSpan? bufferInterval, TimeSpan? measurementWindow)
    {
        // default is 6 measurements/minute
        return sensorSimulator.RunSimulation(
            bufferInterval ?? new TimeSpan(0, 0, 0, 60),
            measurementWindow ?? new TimeSpan(0, 0, 0, 10)
        );
    }
    
    [HttpGet("{sensorId:guid}")]
    public List<Measurement> Get(Guid sensorId) => measurementsRepository.GetForSensor(sensorId).ToList();
}
