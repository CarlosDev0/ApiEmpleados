namespace ApiEmpleados.Data
{
    public class MessageStore
    {
        public List<MessageDto> Messages { get; set; } = new();
    }

    public class MessageDto
    {
        public string From { get; set; }
        public string Text { get; set; }
        public DateTime ReceivedAt { get; set; }
    }

}
