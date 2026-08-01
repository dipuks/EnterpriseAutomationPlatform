namespace EAP.Core
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Status { get; set; } = "Online";
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;
    }
}
