using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Rekognition;
using Amazon.S3;
using Microsoft.Extensions.FileProviders;
using ReceiptProcessor;
using ReceiptProcessor.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
      .WriteTo.Console() // Log to console
      .WriteTo.File(
            path:"logs/log-.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
            rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: LogEventLevel.Information) // Log to daily files
      .CreateLogger();
// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Host.UseSerilog();

// Configure AWS Services
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddAWSService<IAmazonRekognition>();

// Add custom services
builder.Services.AddScoped<IReceiptService, ReceiptService>();

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();