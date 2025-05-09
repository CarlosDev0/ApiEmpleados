namespace ApiEmpleados.Data
{
    using ApiEmpleados.Models;
    using MongoDB.Driver;
    
    public class MongoDBRepo: IMongoDBRepo
    {
        string mongodbUrl="";
        private readonly IConfiguration configuration;
        MongoClient client;
        private IMongoCollection<Question> questionsCollection;
        public MongoDBRepo(IConfiguration _configuration)
        {
            configuration = _configuration;
            mongodbUrl = configuration.GetConnectionString("MongoDB") ?? "";
            client = new MongoClient(mongodbUrl);
             questionsCollection = client.GetDatabase("assessment").GetCollection<Question>("questions");
        }
        public async Task<List<Question>> GetQuestionById(int questionId)
        {
            var filter = Builders<Question>.Filter.Eq(q => q.Id, questionId);
            var answer = await questionsCollection.Find(filter).FirstOrDefaultAsync();
            return new List<Question> {
                answer
            };
            //var dbList = client.ListDatabases().ToList();
            //Console.WriteLine("The list of databases:");
            //foreach (var item in dbList)
            //{
            //    Console.WriteLine(item);
            //}
        }
        public async Task<List<Question>> GetAllQuestions()
        {
            var allQuestions = await questionsCollection.Find(_ => true).ToListAsync();
            return allQuestions;
        }
        public async void InsertOneRecord(Question question)
        {
            await questionsCollection.InsertOneAsync(question);
        }
        public async Task<Boolean> InsertManyRecords(List<Question> listQuestions)
        {
            var options = new InsertManyOptions() { BypassDocumentValidation = true };
            //await questionsCollection.UpdateManyAsync(listQuestions);

            foreach (var doc in listQuestions)
            {
                var filter = Builders<Question>.Filter.Eq(q => q.Id, doc.Id);
                await questionsCollection.ReplaceOneAsync(
                    filter,
                    doc,
                    new ReplaceOptions { IsUpsert = true });
            }
            return true;
        }
    }
}
