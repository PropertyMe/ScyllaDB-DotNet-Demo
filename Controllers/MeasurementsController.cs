using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Models;
using ScyllaDBDemo.Repositories;

namespace ScyllaDBDemo.Controllers;

public class MeasurementsController(IMeasurementsRepository measurementsRepository) : ControllerBase
{
    [HttpGet("measurements")]
    public List<Measurement>  Get(Guid sensorId) => measurementsRepository.GetForSensor(sensorId).ToList();
}