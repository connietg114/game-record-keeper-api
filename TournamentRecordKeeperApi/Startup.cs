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
                context.SaveChanges();
                

                if (context.TournamentTypes.Count() == 0)
                {
                    context.TournamentTypes.Add(new Models.TournamentType
                    {
                        Name = "Round-robin (all-play-all)",
                        Description = "A round-robin tournament (or all-play-all tournament) is a competition in which each contestant meets all other " +
                        "contestants in turn. A round-robin contrasts with an elimination tournament, in which participants are eliminated after a " +
                        "certain number of losses."
                    });

                    context.TournamentTypes.Add(new Models.TournamentType
                    {
                        Name = "Elimination",
                        Description = "A competition in which only the winners of each stage play in the next stage, until one competitor or team is the final winner."
                    });

                    context.TournamentTypes.Add(new Models.TournamentType
                    {
                        Name = "Ladder",
                        Description = "A tournament in which the entrants are listed by name and rank, advancement being by means of challenging and " +
                        "defeating an entrant ranked one or two places higher."
                    });

                }

                context.SaveChanges();

                if (context.Tournaments.Count() == 0)
                {
                    context.Tournaments.Add(new Models.Tournament
                    {
                        tournamentType = context.TournamentTypes.SingleOrDefault(type => type.ID == 1),
                        Name = "TournamentOne"
                    });

                    context.Tournaments.Add(new Models.Tournament
                    {
                        tournamentType = context.TournamentTypes.SingleOrDefault(type => type.ID == 2),
                        Name = "TournamentTwo"
                    });

                }

                context.SaveChanges();
                if (context.GameMatches.Count() == 0)
                {
                    context.GameMatches.Add(new Models.GameMatch
                    {
                        MatchDate = new DateTime(2020, 03, 06),
                        game = context.Games.SingleOrDefault(game => game.ID == 1),
                        tournament = context.Tournaments.SingleOrDefault(t => t.ID == 1)
                    });

                    context.GameMatches.Add(new Models.GameMatch
                    {
                        MatchDate = new DateTime(2020, 08, 10),
                        game = context.Games.SingleOrDefault(game => game.ID == 2),
                        tournament = context.Tournaments.SingleOrDefault(t => t.ID == 2)

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
