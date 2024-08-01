using Microsoft.AspNetCore.Mvc;
using ScyllaDBDemo.Services;

namespace ScyllaDBDemo.Controllers
{
    public class SensorSimulator(ISensorSimulator sensorSimulator) : ControllerBase
    {
        [HttpGet("sensorsimulator")]
        public Task Get(TimeSpan? bufferInterval, TimeSpan? measurementWindow)
        {
            // default is 6 measurements/minute
            return sensorSimulator.RunSimulation(
                bufferInterval ?? new TimeSpan(0, 0, 0, 60),
                measurementWindow ?? new TimeSpan(0, 0, 0, 10)
            );
        }
    }
}
