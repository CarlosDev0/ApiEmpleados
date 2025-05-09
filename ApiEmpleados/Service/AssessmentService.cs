using ApiEmpleados.Data;
using ApiEmpleados.Models;
using MongoDB.Driver;

namespace ApiEmpleados.Service
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDBRepo mongoDBRepo;
        public AssessmentService(IConfiguration _configuration, IMongoDBRepo _mongoDBRepo)
        {
            mongoDBRepo = _mongoDBRepo;
            configuration = _configuration;
            RecordQuestions();
        }
        private async void RecordQuestions()
        {
            var listQuestions = new List<Question>(){
               new Question() { Description= "Does the 'virtual' keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class?"
               ,Id= 0,
               CorrectAnswer = "True",
               Type = "YN",
               Options = "True/False"
               },
               new Question() { Description= "Is React a Framework?"
               ,Id= 1,
               CorrectAnswer = "False",
               Type = "YN",
               Options = "True/False"
               },
               new Question() { Description= "In React components are reusable building blocks?"
               ,Id= 2,
               CorrectAnswer = "True",
               Type = "YN",
               Options = "True/False"
               },
               new Question() { Description= "If we have: int? a = 10; int? b = null; then the result of: a+b is 10?"
               ,Id= 3,
               CorrectAnswer = "False",
               Type = "YN",
               Options = "True/False"
               }
            };
            await mongoDBRepo.InsertManyRecords(listQuestions);
        }
        public async Task<IEnumerable<Question>> GetNextQuestion(int questionId)
        {
            return await new MongoDBRepo(configuration).GetQuestionById(questionId);
            //switch (questionId)
            //{
            //    case 0:
            //        return await Task.FromResult(new List<Question>(){
            //   new Question() { Description= "Does the 'virtual' keyword is used to modify a method, property, indexer, or event declaration and allow for it to be overridden in a derived class?"
            //   ,Id= questionId,
            //   CorrectAnswer = "True",
            //   Type = "YN",
            //   Options = "True/False"
            //   }
            //});
            //    case 1:
            //        return await Task.FromResult(new List<Question>(){
            //   new Question() { Description= "Is React a Framework?"
            //   ,Id= questionId,
            //   CorrectAnswer = "False",
            //   Type = "YN",
            //   Options = "True/False"
            //   }
            //});
            //    case 2:
            //        return await Task.FromResult(new List<Question>(){
            //   new Question() { Description= "In React components are reusable building blocks?"
            //   ,Id= questionId,
            //   CorrectAnswer = "True",
            //   Type = "YN",
            //   Options = "True/False"
            //   }
            //});
            //    default:
            //        return await Task.FromResult(new List<Question>(){
            //   new Question() { Description= "If we have: int? a = 10; int? b = null; then the result of: a+b is 10?"
            //   ,Id= questionId,
            //   CorrectAnswer = "False",
            //   Type = "YN",
            //   Options = "True/False"
            //   }
            //});

            //}
            
        }
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await new MongoDBRepo(configuration).GetAllQuestions();
        }
    }
}
