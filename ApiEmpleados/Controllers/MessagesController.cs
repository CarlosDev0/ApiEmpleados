using Microsoft.AspNetCore.Mvc;

namespace ApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly MessageStore _messageStore;

        public MessagesController(MessageStore store)
        {
            _messageStore = store;
        }

        [HttpGet]
        public ActionResult<List<MessageDto>> GetMessages()
        {
            return Ok(_messageStore.Messages.OrderByDescending(m => m.ReceivedAt));
        }
    }
}
