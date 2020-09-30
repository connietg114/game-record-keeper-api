using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using TournamentRecordKeeperApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TournamentRecordKeeperApi.Data
{
    public class appContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public appContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameMatch> GameMatches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<TournamentType> TournamentTypes { get; set; }
        public DbSet<WinCondition> WinConditions { get; set; }
        public DbSet<FirstOf> FirstOf { get; set; }
        public DbSet<BestOf> BestOf { get; set; }
        public DbSet<Survival> Survivals { get; set; }
    }
}
