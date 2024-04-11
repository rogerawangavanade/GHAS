using OpenAIApp.Congurations;
using OpenAIApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<OpenAIConfig>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<OpenWeatherConfig>(builder.Configuration.GetSection("OpenWeather"));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
             //LOCALCODE
            // .WithOrigins("https://openaichatbotreactservice.azurewebsites.net")
            .WithOrigins("https://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IOpenAIServices, OpenAIServices>();
builder.Services.AddScoped<OpenWeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
