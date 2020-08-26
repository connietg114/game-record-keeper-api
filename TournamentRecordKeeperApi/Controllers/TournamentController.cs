using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentRecordKeeperApi.Data;
using TournamentRecordKeeperApi.Models;

namespace TournamentRecordKeeperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly appContext _Context;
        public TournamentController (appContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Get(int? id = null, string name = null, DateTime? startDate = null,
            DateTime? endDate = null, int? tournamentType = null)
        {
            IQueryable<Tournament> tournaments = _Context.Set<Tournament>();

            if (id != null)
            {
                tournaments = tournaments.Where(t => t.ID == id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                tournaments = tournaments.Where(t => t.Name == name);
            }

            if (startDate != null)
            {
                tournaments = tournaments.Where(t => t.StartDate == startDate);
            }

            if (endDate != null)
            {
                tournaments = tournaments.Where(t => t.EndDate == endDate);
            }

            if (tournamentType != null)
            {
                tournaments = tournaments.Where(t => t.TournamentType == tournamentType);
            }

            return Ok(
                tournaments.ToList()
                );
        }
    }
}
