using Microsoft.EntityFrameworkCore;
using JobPortalBackend.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

/* Add CORS policy to allow Angular frontend (http://localhost:4200)
   and Swagger UI on both HTTP and HTTPS for local development */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        policy =>
        {
            policy.SetIsOriginAllowed(origin => true) // Allow any origin
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Add DbContext with SQLite for local development and online DB usage
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5297; // Or your custom HTTPS port
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Job Portal API", 
        Version = "v1",
        Description = "API for Job Portal Application"
    });
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Job Portal API v1");
    });
}

// Enable CORS for Angular frontend
app.UseCors("AllowAngularDevClient");

// Move UseHttpsRedirection after UseCors to avoid redirect issues with CORS preflight
//app.UseHttpsRedirection();

// Add this middleware to handle OPTIONS requests and avoid CORS preflight issues
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204;
        await context.Response.CompleteAsync();
    }
    else
    {
        await next();
    }
});

app.UseAuthorization();
app.MapControllers();

app.Run();
