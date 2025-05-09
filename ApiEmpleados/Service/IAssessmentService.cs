using ApiEmpleados.Models;

namespace ApiEmpleados.Service
{
    public interface IAssessmentService
    {
        public Task<IEnumerable<Question>> GetNextQuestion(int questionId);
        public Task<IEnumerable<Question>> GetAllQuestions();
    }
}
