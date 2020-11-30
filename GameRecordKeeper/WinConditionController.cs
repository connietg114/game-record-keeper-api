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
    public class WinConditionController : ControllerBase
    {
        private readonly appContext _context;

        public WinConditionController(appContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.WinConditions.ToList());
        }
    }
}
