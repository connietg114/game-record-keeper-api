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

        public IEnumerable<GameMatch> Get(int? id = null)
        {   if (id != null)
            {
                return _context.GameMatches.Include(gm => gm.game).Include(gm => gm.tournament).Include(gm => gm.tournament.tournamentType).Where(g => ((DateTime.Now - g.MatchDate).Days <= 90) && g.ID == id).ToList();
            }
            return _context.GameMatches.Include(gm=>gm.game).Include(gm=>gm.tournament).Include(gm=>gm.tournament.tournamentType).AsEnumerable().Where(g=>(DateTime.Now-g.MatchDate).Days<=90).ToList();
        }
        
        [HttpGet]
        [Route("allmatches")]
        public IEnumerable<GameMatch> GetAllMatches(int? id = null)
        {
            if (id != null)
            {
                return _context.GameMatches.Include(gm => gm.game).Include(gm => gm.tournament).Include(gm => gm.tournament.tournamentType).Where(g => g.ID == id).ToList();
            }
            return _context.GameMatches.Include(gm => gm.game).Include(gm => gm.tournament).Include(gm => gm.tournament.tournamentType).ToList();
        }



    }
}
