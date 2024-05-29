using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IWebHostEnvironment env = builder.Environment;

            // Add services to the container.

            if (env.IsDevelopment())
                builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            else
                builder.Configuration.AddJsonFile("ocelotProd.json", optional: false, reloadOnChange: true);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
