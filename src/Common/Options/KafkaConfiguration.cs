namespace Common.Options
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string IncomingTopic { get; set; }
        public string OutgoingTopic { get; set; }
        public string GroupId { get; set; }
    }
}