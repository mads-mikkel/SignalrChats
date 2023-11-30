using Microsoft.AspNetCore.SignalR.Client;

namespace SignalrChats.ConsoleClient
{
    public class Chat
    {
        string username;
        string hubUrl = "http://localhost:5151/Chat";
        public event EventHandler NewMessageRecieved;
        const string ClientHandlerName = "HandleNewMessage";
        const string ServerHandlerName = "SendMessage";

        // The object representing the Hub on the server:
        HubConnection connection;

        public Chat(string username)
        {
            this.username = username;

            // 1. Make a connection:
            connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
        }

        public async Task Start()
            // 2. Start the connection:
            => connection.StartAsync();

        public async Task Connect()
        {
            (string, string) tuple = default;

            // 3. Register the name of the method 
            connection.On<string, string>(ClientHandlerName, (user, message) =>
            {
                NewMessageRecieved?.Invoke(this, new MessageRecievedEventArgs() { User = user, Message = message });
            });
        }

        public async Task Send(string message)
        {
            await connection.InvokeAsync(ServerHandlerName, username, message);
        }

    }
}