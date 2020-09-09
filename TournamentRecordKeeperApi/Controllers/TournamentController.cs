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
        public IEnumerable<Tournament> Get(int? id = null)
        {
            if (id != null)
            {
                return _Context.Tournaments.Include(t => t.tournamentType).Where(t => ((DateTime.Now - t.StartDate).Days <= 90) && t.ID == id).ToList();
            }
            return _Context.Tournaments.Include(t => t.tournamentType).AsEnumerable().Where(t => ((DateTime.Now - t.StartDate).Days <= 90)).ToList();
        }


        [HttpGet]
        [Route("allTournament")]
        public IEnumerable<Tournament> GetAllTournaments(int? id = null)
        {
            if (id != null)
            {
                return _Context.Tournaments.Include(t => t.tournamentType).Where(t => t.ID == id).ToList();
            }
            return _Context.Tournaments.Include(t => t.tournamentType).ToList();
        }

       
    }
}
