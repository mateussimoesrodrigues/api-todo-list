// Program.cs
using API_ToDo_List.Repositorios;
using Microsoft.Extensions.Configuration; // Certifique-se que este using existe

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();

builder.Services.AddScoped<ITarefaDiariaRepositorio, TarefaDiariaRepositorio>();


// --- SEÇÃO CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(
                            "http://127.0.0.1:5500", 
                            "http://localhost:5500",
                            "http://127.0.0.1:5501"
                         )
                      .AllowAnyMethod()
                      .AllowAnyHeader());
});
// --- FIM SEÇÃO CORS ---

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- APLICAÇÃO DO CORS ---
app.UseCors("AllowSpecificOrigin"); // <<< Certifique-se que esta linha está após UseHttpsRedirection()
// --- FIM APLICAÇÃO DO CORS ---

app.UseAuthorization();
app.MapControllers();

app.Run();