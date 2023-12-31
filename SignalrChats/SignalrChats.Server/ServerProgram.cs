namespace SignalrChats.Server
{
    public class ServerProgram
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddLogging();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<ChatHub>("/Chat");

            app.Run();
        }
    }
}
