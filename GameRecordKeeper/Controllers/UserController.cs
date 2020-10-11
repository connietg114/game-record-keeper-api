using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using GameRecordKeeper.Data;
using GameRecordKeeper.Models;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameRecordKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly appContext _context;

        public UserController(appContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Problem("Unable to get logged in user ID");

            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return Problem("Logged in user not found.");

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.NormalizedUserName,
                user.Email,
                user.NormalizedEmail,
                user.PhoneNumber
            });
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID not provided");

            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return Problem("Logged in user not found.");

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.NormalizedUserName,
                user.Email,
                user.NormalizedEmail,
                user.PhoneNumber
            });
        }
    }
}
