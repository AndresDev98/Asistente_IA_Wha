using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Fynex.Service.WhatsApp.Api.Services
{
    public class TwilioService
    {
        private readonly string _sid;
        private readonly string _token;
        private readonly string _from;

        public TwilioService(IConfiguration config)
        {
            _sid = config["Twilio:AccountSid"] ?? throw new ArgumentNullException("Twilio:AccountSid is missing");
            _token = config["Twilio:AuthToken"] ?? throw new ArgumentNullException("Twilio:AuthToken is missing");
            _from = config["Twilio:From"] ?? throw new ArgumentNullException("Twilio:From is missing");

            TwilioClient.Init(_sid, _token);
        }

        public async Task SendMessageAsync(string to, string message)
        {
            if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Destination and message must be provided");

            var formattedTo = to.StartsWith("whatsapp:") ? to : $"whatsapp:{to}";

            await MessageResource.CreateAsync(
                to: new PhoneNumber(formattedTo),
                from: new PhoneNumber(_from),
                body: message
            );
        }
    }
}
