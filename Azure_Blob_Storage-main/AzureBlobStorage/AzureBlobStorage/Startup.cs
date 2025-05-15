using Azure.Storage.Blobs;
using AzureBlobStorage.AsureStorage;
using AzureBlobStorage.AzureStorage;
using AzureBlobStorage.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace AzureBlobStorage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddSingleton(x => new AzureSettings(
                new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorageConnectionString")),
                Configuration.GetValue<string>("AzureContainerName")));
            services.AddSingleton<IBlobService, BlobService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseSwagger()
                    .UseSwaggerUI(config =>
                    {
                        config.SwaggerEndpoint("/swagger/v1/swagger.json", "Relations.API V1");
                    });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
