using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TournamentRecordKeeperApi.Data;
using TournamentRecordKeeperApi.Models;

[assembly: HostingStartup(typeof(TournamentRecordKeeperApi.Areas.Identity.IdentityHostingStartup))]
namespace TournamentRecordKeeperApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}