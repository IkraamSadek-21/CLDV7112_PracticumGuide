namespace CLDV7112_PracticumGuide.Models
{
    public class SensorReading
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
