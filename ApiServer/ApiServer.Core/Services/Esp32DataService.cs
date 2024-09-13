namespace ApiServer.Core.Services
{
    public class Esp32DataService
    {
        public bool IsScaleConnectedAsync(string scaleName)
        {
            switch (scaleName)
            {
                case "ESP32-001":
                    return true;
                case "ESP32-002":
                    return false;
                default:
                    break;
            }
            return true;
        }
    }
}
