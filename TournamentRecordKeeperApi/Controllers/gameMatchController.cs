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

        public IEnumerable<GameMatch> Get()
        {
            return _context.GameMatches.Include(gm=>gm.game).AsEnumerable().Where(g=>(DateTime.Now-g.MatchDate).Days<=90).ToList();
        }

       

    }
}
