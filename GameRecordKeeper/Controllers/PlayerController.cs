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
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly appContext _context;

        public PlayerController(appContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int? id)
        {
            if (id == null)
                return BadRequest($"Player id not provided.");

            var player = _context.Players.SingleOrDefault(p => p.ID == id);

            if (player == null)
                return NotFound($"Player {id} not found.");

            return Ok(new
                {
                    id = player.ID,
                    preferredName = player.PreferredName,
                    gender = player.Gender,
                    firstName = player.FirstName,
                    lastName = player.LastName
                });
        }

        [HttpGet("")]
        public ActionResult Get(int? skip, int? limit = 10)
        {
            IQueryable<Player> players = _context.Players;

            if (skip != null)
                players = players.Skip(skip.Value);

            players = players.Take(limit.Value);

            return Ok(players.Select(player => new
            {
                id = player.ID,
                preferredName = player.PreferredName,
                gender = player.Gender.ToString(),
                firstName = player.FirstName,
                lastName = player.LastName
            }));
        }
    }
}
