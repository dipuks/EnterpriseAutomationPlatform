using System;
using System.Collections.Generic;
using System.Text;

namespace EAP.Core.Services
{
    public interface ISimulationEngine
    {
        string GenerateResponsePacket(string deviceType, string inputJson);
    }

    public class SimulationEngine : ISimulationEngine
    {
        public string GenerateResponsePacket(string deviceType, string inputJson)
        {
            // Implement the logic to generate a response packet based on the device type and input JSON.
            // This is a placeholder implementation. You can customize it based on your requirements.
            var timestamp = DateTime.UtcNow.ToString("o");
            return $"{{ \"deviceType\": \"{deviceType}\", \"timestamp\": \"{timestamp}\", \"status\": \"OK\", \"packet\": \"SIMULATED_DATA_FROM_{deviceType}_INPUT_{inputJson.Length}\" }}";
        }
    }
}
