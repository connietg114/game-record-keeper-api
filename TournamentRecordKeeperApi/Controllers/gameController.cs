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

        //https://localhost:5001/game/
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return _context.Games.ToList();
        }


        [HttpGet]
        [Route("getGames")]
        public IEnumerable<Game> Get(string i)
        {
            return _context.Games.Where(u => u.Name == i).ToList();
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