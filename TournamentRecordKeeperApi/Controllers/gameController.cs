﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentRecordKeeperApi.Models;
using TournamentRecordKeeperApi.Data;
using Microsoft.EntityFrameworkCore;


namespace TournamentRecordKeeperApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class gameController : ControllerBase
    {
        private readonly appContext _context;

        public gameController(appContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Games.Select(g => new {
                    id = g.ID,
                    name = g.Name,
                    minPlayerCount = g.MinPlayerCount,
                    maxPlayerCount = g.MaxPlayerCount,
                    gameModes = g.GameModes.Count
                }).ToList());       
        }

        [HttpGet]
        [Route("getGameDetails")]
        public ActionResult GetGames(int? id = null)
        {
            if (id == null)
            {
                return BadRequest("Game ID is not provided");
                
            }
            return Ok(_context.Games.Include(g => g.GameModes).SingleOrDefault(g => g.ID == id));
        }

        [HttpPost]
        public IActionResult post(Game item)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Not a valid model");
            //if (string.IsNullOrEmpty(item.Name))
            //    return BadRequest("No name is provided");
            //if (item == null)
            //    return BadRequest("No data is provided");
            //if ID already exists?

            _context.Games.Add(new Game()
            {
               
                Name = item.Name,
                MinPlayerCount = item.MinPlayerCount,
                MaxPlayerCount = item.MaxPlayerCount

            });

            _context.SaveChanges();
            return Ok();
        }       


    }
}
