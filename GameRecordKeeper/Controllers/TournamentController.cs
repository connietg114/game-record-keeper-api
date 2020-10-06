using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameRecordKeeper.Data;
using GameRecordKeeper.Models;

namespace GameRecordKeeper.Controllers
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


        [HttpPut]
        [Route("postNewTournament")]
        public IActionResult PostNewTournament(Tournament tournament, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            var type = _Context.TournamentTypes.SingleOrDefault(tt => tt.ID == id);
            if (type == null)
            {
                return BadRequest("Tournament Type ID is not found");
            }


            _Context.Tournaments.Add(new Tournament()
            {
                Name = tournament.Name,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                tournamentType = type
            }); ;

                _Context.SaveChanges();
            


            return Ok(
                new
                {
                    message = "OK"
                });
        }

        [HttpDelete]
        [Route("deleteTournament")]
        public IActionResult DeleteTournament(int? id = null)
        {
            if (id == null)
            {
                return BadRequest("Tournament ID is not provided");
            }

            var removeItem = _Context.Tournaments.SingleOrDefault(t => t.ID == id);

            if (removeItem == null)
            {
                return BadRequest("Tournament ID is not found");
            }

            _Context.Remove(removeItem);
            _Context.SaveChanges();

            return Ok(new
            {
                message = "OK"
            });
        }
    }
}
