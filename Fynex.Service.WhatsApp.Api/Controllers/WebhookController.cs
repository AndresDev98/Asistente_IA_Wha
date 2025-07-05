using Fynex.Service.WhatsApp.Api.Models;
using Fynex.Service.WhatsApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fynex.Service.WhatsApp.Api.Controllers
{
    [ApiController]
    [Route("api/webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly OpenAiService _ai;
        private readonly TwilioService _twilio;

        public WebhookController(OpenAiService ai, TwilioService twilio)
        {
            _ai = ai;
            _twilio = twilio;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WhatsAppMessage msg)
        {
            var reply = await _ai.GetReplyAsync(msg.Body);
            await _twilio.SendMessageAsync(msg.From, reply);
            return Ok();
        }
    }

}
