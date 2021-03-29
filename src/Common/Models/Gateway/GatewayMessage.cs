using Common.Commands;
using Newtonsoft.Json;

namespace Common.Models.Gateway
{
    public class GatewayMessage
    {
        public string Command { get; set; }

        public string CommandName { get; set; }

        public GatewayMessage() { }
        
        public GatewayMessage(Command command)
        {
            Command = JsonConvert.SerializeObject(command);;
            CommandName = command.GetType().Name;
        }
    }
}