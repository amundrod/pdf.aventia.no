using Hangfire;
using Microsoft.OpenApi.Models;
using pdf.aventia.no.Services;
using System.Reflection;

// Setting the IronPdf license key
IronPdf.License.LicenseKey = "IRONPDF.SANDERHALVORSEN.32719-F3E81AAF6B-FXN2WN-GE6OLLL3GSUJ-7223ULMFJBDU-UKMLSCOIKAY2-J5LEMVSQG4C4-ALOMPQB4HGAD-EQHHZK-TECCJDW5AQ2JUA-DEPLOYMENT.TRIAL-UHL6EQ.TRIAL.EXPIRES.03.JUN.2023";

// Building the web application
var builder = WebApplication.CreateBuilder(args);

// Adding services to the application
builder.Services.AddServices(builder.Environment, builder.Configuration);

// Adding controllers
builder.Services.AddControllers();

// Adding API explorer endpoints
builder.Services.AddEndpointsApiExplorer();

// Setting up Swagger for API documentation
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aventia PDF API", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
        {
            c.IncludeXmlComments(xmlPath);
        }
    });

// Building the application
var app = builder.Build();

// Enabling Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Enabling HTTPS redirection
app.UseHttpsRedirection();

// Enabling authorization
app.UseAuthorization();

// Mapping controllers
app.MapControllers();

// Setting up Hangfire dashboard
app.UseHangfireDashboard();

// Adding Hangfire recurring job for processing PDF files
RecurringJob.AddOrUpdate<PdfService>("ProcessPdfFiles", t => t.ProcessPdfFiles(CancellationToken.None), Cron.Minutely);

// Running the application
app.Run();