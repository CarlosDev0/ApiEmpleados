namespace ApiEmpleados.Dtos
{
    public class AnswerAuthentication
    {
        public Boolean Answer;
        public string status;
        public string sessionToken;
        public string user;
        public Guid? empleadoId;
        public string? nombre;
    }
}
