namespace ApiEmpleados.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Options { get; set; } = string.Empty;
    }
}
