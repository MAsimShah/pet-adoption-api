var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Registers Swagger services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables the middleware to serve the generated Swagger as JSON

    // Enables the Swagger UI at /swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Adoption");
        c.RoutePrefix = string.Empty; // Optional: serve Swagger at root
    });       
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
