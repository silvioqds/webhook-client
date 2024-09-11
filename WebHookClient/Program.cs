using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;


const string server = "http://localhost:5042/WebHook";
const string callback = "http://localhost:5286/webhookclient/item/new";
const string topic = "item.new";
var client = new HttpClient();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();



builder.Services.AddW3CLogging(log =>
{
    log.LoggingFields = Microsoft.AspNetCore.HttpLogging.W3CLoggingFields.All;
    log.FlushInterval = TimeSpan.FromSeconds(2);
});

var app = builder.Build();
app.UseW3CLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

//Mando minha inscricão para o servidor webhook
await client.PostAsJsonAsync(server + "/subscribe", new { topic, callback });
Console.WriteLine($"subscrito no tópico {topic} com o callback {callback}");

app.Run();
