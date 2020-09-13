using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentRecordKeeperApi.Models;
using TournamentRecordKeeperApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace TournamentRecordKeeperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gameMatchController : ControllerBase
    {
        private readonly appContext _context;
    
        public gameMatchController(appContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get(DateTime? startDate = null, DateTime? endDate = null)
        {
            var gameMatches = _context.GameMatches
                .Include(gm => gm.game)
                .Include(gm => gm.tournament)
                .Include(gm => gm.tournament.tournamentType)
                .AsQueryable();

            if (startDate != null)
            {
                gameMatches = gameMatches.Where(g => g.MatchDate > startDate);
            }

            if (endDate != null)
            {
                gameMatches = gameMatches.Where(g => g.MatchDate > endDate);
            }
            return Ok(gameMatches.ToList());
        }
        
        [HttpGet]
        [Route("getGameMatchDetails")]
        public ActionResult GetGameMatchDetails(int? id = null)
        {
            if (id == null)
            {
                return BadRequest("Game Match ID is not provided");
            }
            return Ok(_context.GameMatches
                .Include(gm => gm.game)
                .Include(gm => gm.tournament)
                .Include(gm => gm.tournament.tournamentType)
                .SingleOrDefault(g => g.ID == id));
        }

    }
}
