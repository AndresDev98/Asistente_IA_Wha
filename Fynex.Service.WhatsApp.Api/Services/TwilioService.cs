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
            _sid = config["Twilio:AccountSid"];
            _token = config["Twilio:AuthToken"];
            _from = config["Twilio:From"];
            TwilioClient.Init(_sid, _token);
        }

        public async Task SendMessageAsync(string to, string message)
        {
            await MessageResource.CreateAsync(
                to: new PhoneNumber($"whatsapp:{to}"),
                from: new PhoneNumber(_from),
                body: message
            );
        }
    }

}
