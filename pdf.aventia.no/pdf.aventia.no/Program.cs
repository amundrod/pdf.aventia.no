using Hangfire;
using Microsoft.OpenApi.Models;
using pdf.aventia.no.Services;
using System.Reflection;


IronPdf.License.LicenseKey = "IRONPDF.SANDERHALVORSEN.32719-F3E81AAF6B-FXN2WN-GE6OLLL3GSUJ-7223ULMFJBDU-UKMLSCOIKAY2-J5LEMVSQG4C4-ALOMPQB4HGAD-EQHHZK-TECCJDW5AQ2JUA-DEPLOYMENT.TRIAL-UHL6EQ.TRIAL.EXPIRES.03.JUN.2023";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Environment, builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

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

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<PdfService>("ProcessPdfFiles", t => t.ProcessPdfFiles(CancellationToken.None), Cron.Minutely);

app.Run();