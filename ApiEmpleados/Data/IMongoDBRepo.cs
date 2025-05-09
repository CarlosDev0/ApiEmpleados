using ApiEmpleados.Models;

namespace ApiEmpleados.Data
{
    public interface IMongoDBRepo
    {
        public Task<List<Question>> GetQuestionById(int questionId);
        public  Task<List<Question>> GetAllQuestions();
        public void InsertOneRecord(Question question);
        public Task<Boolean> InsertManyRecords(List<Question> listQuestions);
    }
}
