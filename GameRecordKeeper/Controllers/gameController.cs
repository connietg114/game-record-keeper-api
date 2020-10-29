using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GameRecordKeeper.Models;
using GameRecordKeeper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;

namespace GameRecordKeeper.Controllers
{
    public enum SortOrder
    {
        Ascending,
        Desceding
    }

    public class SortDescription
    {
        public string FieldName { get; set; }
        public int Priority { get; set; }
        public SortOrder Direction { get; set; }
    }

    public class GamesRequest
    {
        public int? page { get; set; }
        public int? rowsPerPage { get; set; }
        public List<string> sortItems { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class gameController : ControllerBase
    {
        private readonly appContext _context;

        public gameController(appContext context)
        {
            _context = context;
        }
        //key=sort, value=gameID, Name
        // page=?&rowsPerPage=?&sortBy=Name&SortDirection&Ascending&sortBy=Id&SortDirection=Desc

        [HttpPost, Route("/api/Games")]
        //post can only pass in a class/complex object for parameter
        public ActionResult Get(GamesRequest request)
        {   ///filter
            //sort
          

            int? page = request.page;
            int? rowsPerPage = request.rowsPerPage;
            string[] sortItems = request.sortItems?.ToArray();

            var games =_context.Games.AsQueryable();

            //https://stackoverflow.com/questions/13527657/sorting-with-orderby-thenby
            if (sortItems != null)
            {
                IOrderedQueryable<Game> orderedGames = null;
                for (int i = 0; i < sortItems.Length; i++)
                {   
                    var item = sortItems[i];
                    if (i == 0)
                    {
                        if (item == "ID")
                        {
                            orderedGames = games.OrderBy(g => g.ID);
                        }
                        else if (item == "ID desc")
                        {
                            orderedGames = games.OrderByDescending(g => g.ID);
                        }
                        else if (item == "Name")
                        {
                            orderedGames = games.OrderBy(g => g.Name);
                        }
                        else if (item == "Name desc")
                        {
                            orderedGames = games.OrderByDescending(g => g.Name);
                        }
                        else if (item == "MinPlayerCount")
                        {
                            orderedGames = games.OrderBy(g => g.MinPlayerCount);
                        }
                        else if (item == "MinPlayerCount desc")
                        {
                            orderedGames = games.OrderByDescending(g => g.MinPlayerCount);
                        }
                        else if (item == "MaxPlayerCount")
                        {
                            orderedGames = games.OrderBy(g => g.MaxPlayerCount);
                        }
                        else if (item == "MaxPlayerCount desc")
                        {
                            orderedGames = games.OrderByDescending(g => g.MaxPlayerCount);
                        }
                        else if (item == "GameModeCount")
                        {
                            orderedGames = games.OrderBy(g => g.GameModes.Count);
                        }
                        else if (item == "GameModeCount desc")
                        {
                            orderedGames = games.OrderByDescending(g => g.GameModes.Count);
                        }
                    }
                    else
                    {
                        if (item == "ID")
                        {
                            orderedGames = orderedGames.ThenBy(g => g.ID);
                        }
                        else if (item == "ID desc")
                        {
                            orderedGames = orderedGames.ThenByDescending(g => g.ID);
                        }
                        else if (item == "Name")
                        {
                            orderedGames = orderedGames.ThenBy(g => g.Name);
                        }
                        else if (item == "Name desc")
                        {
                            orderedGames = orderedGames.ThenByDescending(g => g.Name);
                        }
                        else if (item == "MinPlayerCount")
                        {
                            orderedGames = orderedGames.ThenBy(g => g.MinPlayerCount);
                        }
                        else if (item == "MinPlayerCount desc")
                        {
                            orderedGames = orderedGames.ThenByDescending(g => g.MinPlayerCount);
                        }
                        else if (item == "MaxPlayerCount")
                        {
                            orderedGames = orderedGames.ThenBy(g => g.MaxPlayerCount);
                        }
                        else if (item == "MaxPlayerCount desc")
                        {
                            orderedGames = orderedGames.ThenByDescending(g => g.MaxPlayerCount);
                        }
                        else if (item == "GameModeCount")
                        {
                            orderedGames = orderedGames.ThenBy(g => g.GameModes.Count);
                        }
                        else if (item == "GameModeCount desc")
                        {
                            orderedGames = orderedGames.ThenByDescending(g => g.GameModes.Count);
                        }
                    }
                    
                }
                if (sortItems.Length > 0) {
                    games = orderedGames;
                }
                
            }


            //pagination
            if (page == null && rowsPerPage == null)
            {
                return Ok(games.Select(g => new {
                    id = g.ID,
                    name = g.Name,
                    minPlayerCount = g.MinPlayerCount,
                    maxPlayerCount = g.MaxPlayerCount,
                    gameModes = g.GameModes.Count
                }).ToList());
            }

            int pageNo = page.Value;
            int rows = rowsPerPage.Value;
            return Ok(new
            {
                games = games.AsQueryable()
                .Skip(pageNo * rows)
                .Take(rows)
                .Select(g => new
                {
                    id = g.ID,
                    name = g.Name,
                    minPlayerCount = g.MinPlayerCount,
                    maxPlayerCount = g.MaxPlayerCount,
                    gameModes = g.GameModes.Count
                }).ToList(),
                total = games.Count()
            });
            


        }

        [HttpGet]
        [Route("getGameDetails")]
        public ActionResult GetGames(int? id = null)
        {
            if (id == null)
            {
                return BadRequest("Game ID is not provided");
                
            }
            return Ok(_context.Games
                .Include(g => g.GameModes)
                .Include(g => g.GameModes).ThenInclude(gm=>gm.winCondition)
                .SingleOrDefault(g => g.ID == id));
        }

        [HttpPost]

        public IActionResult Post(Game item)
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
            return Ok(
                new
                {
                    message = "Item is posted"
                });
        }

        [HttpDelete]
        public ActionResult Delete(int? id=null)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Game not found.");

                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model.");
                }else if (_context.GameMatches.Where(g=>g.game.ID==id).ToList().Count!=0)
                {
                    return BadRequest("Game " + id + " exists in Game Match(es).");
                }

                _context.Games.Remove(_context.Games.SingleOrDefault(g => g.ID == id));
                _context.SaveChanges();
                return Ok("Game is deleted");
                //return Ok(new {
                //    message = "Item is deleted"
                //});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                //Value cannot be null. (Parameter 'entity')
            }

        }

        [HttpDelete]
        [Route("deleteMultipleGames")]
        public ActionResult DeleteGames(int[] ids=null)
        {
            try
            {
                if (ids == null)
                {
                    return BadRequest("Games not found");

                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model");
                }

                foreach(int id in ids) 
                {
                   var item = _context.Games.SingleOrDefault(g => g.ID == id);
                    if (item == null)
                    {
                        return BadRequest("Game not found");
                    }
                    _context.Games.Remove(item);
                }
                
                _context.SaveChanges();
                return Ok("Games are deleted");
                //return Ok(new
                //{
                //    message = "Items are deleted"
                //});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
               
            }

        }

    }
}
