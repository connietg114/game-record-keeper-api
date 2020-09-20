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
        public ActionResult Get(DateTime? startDate = null, DateTime? endDate = null)
        {
            var tournaments = _Context.Tournaments
                .Include(t => t.tournamentType)
                .AsQueryable();

            if (startDate != null)
            {
                tournaments = tournaments.Where(t => t.StartDate > startDate);
            }

            if (endDate != null)
            {
                tournaments = tournaments.Where(t => t.EndDate > endDate);
            }

            return Ok(tournaments.ToList());
        }

        [HttpGet]
        [Route("getTournamentDetails")]
        public ActionResult GetTournamentDetails(int? id = null)
        {
            if (id == null)
            {
                return BadRequest("Tournament ID is not provided");
            }

            return Ok(_Context.Tournaments.Include(t => t.tournamentType).SingleOrDefault(t => t.ID == id));
        }

    }
}
