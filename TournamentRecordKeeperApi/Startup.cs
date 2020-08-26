using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TournamentRecordKeeperApi.Data;
using Newtonsoft;


namespace TournamentRecordKeeperApi
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
            services.AddDbContext<appContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
            //services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "MyOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<appContext>();
                context.Database.Migrate();

               if (context.Games.Count() == 0)
                {
                    context.Games.Add(new Models.Game
                    {
                        Name = "Catan",
                        MinPlayerCount = 2,
                        MaxPlayerCount = 4
                    });

                    context.Games.Add(new Models.Game
                    {
                        Name = "Star Realms",
                        MinPlayerCount = 1,
                        MaxPlayerCount = 4
                    });
                    
                }

                if (context.GameMatches.Count() == 0)
                {
                    context.GameMatches.Add(new Models.GameMatch
                    {
                        MatchDate = new DateTime(2020, 03, 06),
                        game = context.Games.SingleOrDefault(game => game.ID == 1)
                    });

                    context.GameMatches.Add(new Models.GameMatch
                    {
                        MatchDate = new DateTime(2020, 08, 10),
                        game = context.Games.SingleOrDefault(game => game.ID == 2)

                    });

                }

			if (context.Tournaments.Count() == 0)
                {
                    context.Tournaments.Add(new Models.Tournament
                    {
                        //ID = 00001,
                        Name = "TournamentOne"
                    });

                    context.Tournaments.Add(new Models.Tournament
                    {
                        //ID = 00002,
                        Name = "TournamentTwo"
                    });

                }               

                context.SaveChanges();

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
