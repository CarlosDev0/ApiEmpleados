using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace ApiEmpleados.Controllers
{
    [ApiController]
    [Route("webhook")]


    public class WebhookController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly MessageStore _messageStore;

        public WebhookController(IHttpClientFactory httpClientFactory, IConfiguration config, MessageStore messageStore)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _messageStore = messageStore;
        }

        [HttpGet]
        public IActionResult VerifyWebhook([FromQuery(Name = "hub.mode")] string mode,
                                           [FromQuery(Name = "hub.verify_token")] string token,
                                           [FromQuery(Name = "hub.challenge")] string challenge)
        {
            if (mode == "subscribe" && token == _config["Meta:WebhookVerifyToken"])
            {
                return Ok(challenge);
            }

            return Forbid();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyWebhook([FromBody] JsonElement body)
        {
            Console.WriteLine("Incoming webhook message:");
            Console.WriteLine(JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true }));

            var message = body.GetProperty("entry")[0]
                              .GetProperty("changes")[0]
                              .GetProperty("value")
                              .GetProperty("messages")[0];

            if (message.GetProperty("type").GetString() == "text")
            {
                var from = message.GetProperty("from").GetString();
                var text = message.GetProperty("text").GetProperty("body").GetString();
                var messageId = message.GetProperty("id").GetString();
                var phoneNumberId = body.GetProperty("entry")[0]
                                        .GetProperty("changes")[0]
                                        .GetProperty("value")
                                        .GetProperty("metadata")
                                        .GetProperty("phone_number_id")
                                        .GetString();

                // Echo reply
                var client = _httpClientFactory.CreateClient();
                var graphApiToken = _config["Meta:GraphApiToken"];
                var url = $"https://graph.facebook.com/v18.0/{phoneNumberId}/messages";

                var payload = new
                {
                    messaging_product = "whatsapp",
                    to = from,
                    text = new { body = "Echo: " + text },
                    context = new { message_id = messageId }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", graphApiToken);
                request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                await client.SendAsync(request);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] JsonElement body)
        {
            var message = body.GetProperty("entry")[0]
                              .GetProperty("changes")[0]
                              .GetProperty("value")
                              .GetProperty("messages")[0];

            if (message.GetProperty("type").GetString() == "text")
            {
                var from = message.GetProperty("from").GetString();
                var text = message.GetProperty("text").GetProperty("body").GetString();

                _messageStore.Messages.Add(new MessageDto
                {
                    From = from,
                    Text = text,
                    ReceivedAt = DateTime.UtcNow
                });

                // (Optional) Send reply...
            }

            return Ok();
        }
    }
}