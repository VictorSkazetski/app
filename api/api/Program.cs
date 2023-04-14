using api.Error;
using api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using api.Infrastructure.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var allowSpecificOrigins = "allowSpecificOrigins";
var connectionString = 
    builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApiContext>(options => 
    options.UseSqlServer(connectionString));
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.ConfigurationMediatR();
builder.Services.AddServices();
builder.Services.AddCors(options =>
    options.AddPolicy(allowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors(allowSpecificOrigins);
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            new KeyValuePair<string, StringValues>("Access-Control-Allow-Origin", "*"));
        ctx.Context.Response.Headers.Append(
            new KeyValuePair<string, StringValues>(
                "Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept"));
    },
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
    RequestPath = new PathString("/images"),
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
