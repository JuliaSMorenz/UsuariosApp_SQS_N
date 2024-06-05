using UsuariosApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(map => map.LowercaseUrls = true);
builder.Services.AddSwaggerDoc();
builder.Services.AddCorsConfig();
builder.Services.AddDependencyInjection();
builder.Services.AddJwtBearerConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorsConfig();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
