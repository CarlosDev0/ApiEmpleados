using ApiEmpleados.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService assessmentService;
        public AssessmentController(IAssessmentService _assessmentService)
        {
            assessmentService = _assessmentService;
        }
        [HttpGet("getQuestion")]
        public async Task<IEnumerable<Question>> GetQuestion(int questionId)
        {
            return await assessmentService.GetNextQuestion(questionId);
        }
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await assessmentService.GetAllQuestions();
        }
    }
}
