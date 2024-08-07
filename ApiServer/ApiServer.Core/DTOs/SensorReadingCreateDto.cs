namespace ApiServer.Core.DTOs;

    public class SensorReadingCreateDto
    {
        public string EspName { get; set; }
        public string EspId { get; set; }
        public int Value { get; set; }
    }

