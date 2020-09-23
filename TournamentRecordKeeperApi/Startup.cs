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
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TournamentRecordKeeperApi.Data;
using TournamentRecordKeeperApi.Models;

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

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Game Record Keeper API",
                    Description = "API to retrieve and update records of games"
                });

            });
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
                    { Name = "Catan",
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


                if (context.Tournaments.Count() == 0)
                {
                    context.Tournaments.Add(new Models.Tournament
                    {
                        Name = "TournamentOne",
                        tournamentType = context.TournamentTypes.SingleOrDefault(tt => tt.Name == "Ladder")
                    });

                    context.Tournaments.Add(new Models.Tournament
                    {
                        Name = "TournamentTwo",
                        tournamentType = context.TournamentTypes.SingleOrDefault(tt => tt.Name == "Round-robin (all-play-all)")
                    });

                }

                context.SaveChanges();
                foreach ( var entity in context.Tournaments.Include(z => z.tournamentType).Where(z => z.tournamentType == null).ToList())
                {
                    entity.tournamentType = context.TournamentTypes.SingleOrDefault(tt => tt.Name == "Ladder");
                }

                
                context.SaveChanges();

                foreach (var entity in context.GameMatches.Include(gm => gm.tournament).Where(gm => gm.tournament == null).ToList())
                {
                    entity.tournament = context.Tournaments.SingleOrDefault(t => t.ID == 2);
                }

                context.SaveChanges();

                foreach (var entity in context.Tournaments.ToList())
                {
                    entity.StartDate = new DateTime(2020, 08, 10);
                }

                context.SaveChanges();

                foreach (var entity in context.Tournaments.ToList())
                {
                    entity.EndDate = new DateTime(2020, 09, 01);
                }

                context.SaveChanges();

                if (context.WinConditions.Count() == 0)
                {
                    context.WinConditions.Add(new Models.WinCondition
                    {
                        Name = "Best Of",
                        Description = "Number of best teams/players out of the number of matches. For example, it can be best 2 out of 3 matches."
                    });

                    context.WinConditions.Add(new Models.WinCondition
                    {
                        Name = "First Of",
                        Description = "First teams/players to achieve some goals. For example, it can be first to get to 20 points."
                    });

                    context.WinConditions.Add(new Models.WinCondition
                    {
                        Name = "Survival",
                        Description = "It records who is the last to be eliminated from the game."
                    });

                    context.WinConditions.Add(new Models.WinCondition
                    {
                        Name = "Highest Score",
                        Description = "Which team/player obtain the hightst score."
                    });
                }

                context.SaveChanges();
                if (context.GameModes.Count() == 0)
                {
                    context.GameModes.Add(new GameMode
                    {
                        game = context.Games.SingleOrDefault(g => g.ID == 1),
                        Name = "GameMode1",
                        winCondition = context.WinConditions.SingleOrDefault(w=>w.ID==1),
                        

                    });
                    context.GameModes.Add(new GameMode
                    {
                        game = context.Games.SingleOrDefault(g => g.ID == 1),
                        Name = "GameMode2",
                        winCondition = context.WinConditions.SingleOrDefault(w => w.ID == 2)

                    });
                }
                context.SaveChanges();


            }
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameRecordKeeper API V1");
                c.RoutePrefix = string.Empty;
            });

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
