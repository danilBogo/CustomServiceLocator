using CustomDependencyInjectionContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CustomDIContainer>();

var app = builder.Build();

var customDiContainer  = app.Services.GetService<CustomDIContainer>();
app.Use(async (_, next) =>
{
    customDiContainer?.ResetScoped();
    await next.Invoke();
});

customDiContainer?.AddTransient(typeof(CounterTransient));
customDiContainer?.AddScoped(typeof(CounterScoped));
customDiContainer?.AddSingleton(typeof(CounterSingleton));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
