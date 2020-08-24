using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TournamentRecordKeeperApi.Models;

namespace TournamentRecordKeeperApi.Data
{
    public class appContext: DbContext
    {
        public appContext(DbContextOptions options) : base(options) { }
        public DbSet<Game> Games { get; set; }  
        public DbSet<Tournament> Tournaments { get; set; }


        
    }
}
