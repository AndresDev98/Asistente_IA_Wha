using Fynex.Service.WhatsApp.Api.Models;
using Fynex.Service.WhatsApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fynex.Service.WhatsApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly TwilioService _twilioService;
        private readonly OpenAiService _openAiService;

        public WhatsAppController(TwilioService twilioService, OpenAiService openAiService)
        {
            _twilioService = twilioService;
            _openAiService = openAiService;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveMessage([FromForm] string Body, [FromForm] string From)
        {
            if (string.IsNullOrEmpty(Body) || string.IsNullOrEmpty(From))
                return BadRequest("Missing message or sender");

            // Llama a OpenAI
            var response = await _openAiService.SendMessageAsync(Body);

            // Responde por WhatsApp
            await _twilioService.SendMessageAsync(From, response);

            return Ok();
        }
    }
}

