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
    public Task Get(int? bufferIntervalSeconds, int? measurementWindowSeconds)
    {
        // default is 6 measurements/minute
        var bufferInterval = new TimeSpan(0, 0, bufferIntervalSeconds ?? 60);
        var measurementWindow = new TimeSpan(0, 0, measurementWindowSeconds ?? 10);

        // start as a background process
        sensorSimulator.RunSimulation(bufferInterval, measurementWindow);

        return Task.CompletedTask;
    }
    
    [HttpGet("{sensorId:guid}")]
    public List<Measurement> Get(Guid sensorId) => measurementsRepository.GetForSensor(sensorId).ToList();
}
