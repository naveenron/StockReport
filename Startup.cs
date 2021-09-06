using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Plain.RabbitMQ;
using Prometheus;
using RabbitMQ.Client;
using StockReport.Infrastructure;
using StockReport.Model;
using StockReport.Service;

namespace StockReport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<RabbitMqStockReportDbSettings>(Configuration.GetSection(nameof(RabbitMqStockReportDbSettings)));
            services.AddSingleton<IRabbitMqStockReportDbSettings>(sp => sp.GetRequiredService<IOptions<RabbitMqStockReportDbSettings>>().Value);
            services.AddControllers();
            services.AddSingleton<RabbitMqService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockReport", Version = "v1" });
            });

            services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@104.42.63.117:5672"));
            services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
                    "report_exchange",
                    "report-queue",
                    "report.*",
                    ExchangeType.Topic));

            services.AddSingleton<IMemoryReportStroage, MemoryReportStroage>();
            services.AddHostedService<ReportDataCollector>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockReport v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMetricServer();

            app.UseHttpMetrics();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}