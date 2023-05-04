using Hangfire;
using Microsoft.OpenApi.Models;
using pdf.aventia.no.Services;
using System.Reflection;

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