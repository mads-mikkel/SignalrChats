namespace SignalrChats.ConsoleClient
{
    internal class ClientProgram
    {
        static string username;

        static async Task Main(string[] args)
        {
            await DisplayMessage("Chat", "Welcome to the chat.");
            await DisplayMessage("Chat", "Your username:");
            username = await Console.In.ReadLineAsync();
            await DisplayMessage("Chat", $"Thank you, {username}. The chat will start now.");

            try
            {
                await DisplayMessage("Chat", "Connecting to Chat Hub...");
                Chat chat = new(username);
                chat.NewMessageRecieved += OnNewMessageRecieved;
                await chat.Start();
                await chat.Connect();

                await DisplayMessage("Chat", "You are now connected.");

                while(true)
                {
                    string message = await AwaitInput();
                    if(message != String.Empty)
                    {
                        await chat.Send(message);
                    }
                }
            }
            catch(Exception e)
            {
                await DisplayMessage("Error", $"ERROR: {e.Message}");
            }
            Console.ReadLine();
        }

        static async Task<string> AwaitInput()
        {
            await DisplayMessage(username, "> ", stayOnSameLine: true);
            string input = await Console.In.ReadLineAsync();
            return input;
        }

        static async void OnNewMessageRecieved(object sender, EventArgs e)
        {
            MessageRecievedEventArgs args = e as MessageRecievedEventArgs;
            await DisplayMessage(args.User, args.Message);
        }

        static async Task DisplayMessage(string user, string message, bool stayOnSameLine = false)
        {
            var now = DateTime.Now.ToString("HH:mm:ss");
            string output = $"[{now}] {user}> {message}";

            if(user.Equals("Server"))
                Console.ForegroundColor = ConsoleColor.White;
            else if(user.Equals("Chat"))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if(user.Equals("Error"))
                Console.ForegroundColor = ConsoleColor.Red;
            else if(user.Equals(username))
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            if(stayOnSameLine)
                await Console.Out.WriteAsync(message);
            else
                await Console.Out.WriteLineAsync(output);
        }
    }
}