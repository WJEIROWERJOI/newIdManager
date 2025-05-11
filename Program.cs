using newIdManager.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppIdentity(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAppServices();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.ConfigureMiddleware();

app.Run();
