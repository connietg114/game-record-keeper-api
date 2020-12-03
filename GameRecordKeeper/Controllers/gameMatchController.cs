using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameRecordKeeper.Models;
using GameRecordKeeper.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace GameRecordKeeper.Controllers
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

        public class GameMatchRequest
        {
            public DateTime? startDate { get; set; }
            public DateTime? endDate { get; set; }
        }

        [HttpPost]
        public ActionResult Get(GameMatchRequest gameMatchRequest)
        {
            var startDate = gameMatchRequest.startDate;
            var endDate = gameMatchRequest.endDate;

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
                .Include(gm => gm.game.GameModes).ThenInclude(gm => gm.winCondition)
                .SingleOrDefault(g => g.ID == id));
        }

        [HttpDelete]
        public ActionResult Delete(int? id = null)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Game Match not found.");

                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest("This is not a valid model.");

                }
                //else if(_context.GameMatches.SingleOrDefault(g => g.ID == id)== null)
                //{
                //    return BadRequest("Game Match does not exist.");
                //}
                

                _context.GameMatches.Remove(_context.GameMatches.SingleOrDefault(g => g.ID == id));
                _context.SaveChanges();
                return Ok("Game Match is deleted");
              
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //Value cannot be null. (Parameter 'entity')
            }

        }

    }
}
