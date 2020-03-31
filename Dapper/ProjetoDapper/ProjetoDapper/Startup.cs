using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoDapper.Repository;

namespace ProjetoDapper
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
            //Injeção de dependência
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IContatoRepository, ContatoRepository>();

            //Conceito de lifetimes ou "tempo de vidas"

            // 1. - Transient : Criado a cada vez que são solicitados.
            // 2. - Scoped:     Criado uma vez por solicitação.
            // 3. - Singleton:  Criado na primeira vez que são solicitados. Cada solicitação subseqüente usa a instância que foi criada na primeira vez.


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
