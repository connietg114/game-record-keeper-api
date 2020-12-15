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

    public class FilterItems
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int MinPlayerCount { get; set; }
        public int MaxPlayerCount { get; set; }
        public int GameModeCount { get; set; }
    }

    public class GamesRequest
    {
        public int? page { get; set; }
        public int? rowsPerPage { get; set; }
        public List<string> sortItems { get; set; }
        public List<string> filterItems { get; set; }
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

        [HttpPost, Route("/api/Games")]//post can only pass in a class/complex object for parameter
        public ActionResult Get(GamesRequest request)
        {
            int? page = request.page;
            int? rowsPerPage = request.rowsPerPage;
            string[] sortItems = request.sortItems?.ToArray();
            string[] filterItems = request.filterItems?.ToArray();

            var games = _context.Games.AsQueryable();

            //filter
            if (filterItems != null)
            {
                for (int i = 0; i < filterItems.Length; i++)
                {
                    var item = filterItems[i];
                    string[] splitItem = item.Split();

                    if (splitItem.Length == 3)
                    {
                        var value = int.Parse(splitItem[2]);

                        if (splitItem[0] == "MinPlayerCount")
                        {
                            if (splitItem[1] == "min")
                            {
                                games = games.Where(g => g.MinPlayerCount >= value);
                            }
                            else if (splitItem[1] == "max")
                            {
                                games = games.Where(g => g.MinPlayerCount <= value);
                            }

                        }

                        else if (splitItem[0] == "MaxPlayerCount")
                        {
                            if (splitItem[1] == "min")
                            {
                                games = games.Where(g => g.MaxPlayerCount >= value);
                            }
                            else if (splitItem[1] == "max")
                            {
                                games = games.Where(g => g.MaxPlayerCount <= value);
                            }

                        }
                        else if (splitItem[0] == "GameId")
                        {
                            if (splitItem[1] == "min")
                            {
                                games = games.Where(g => g.ID >= value);
                            }
                            else if (splitItem[1] == "max")
                            {
                                games = games.Where(g => g.ID <= value);
                            }

                        }

                        else if (splitItem[0] == "GameModeCount")
                        {
                            if (splitItem[1] == "min")
                            {
                                games = games.Where(g => g.GameModes.Count >= value);
                            }
                            else if (splitItem[1] == "max")
                            {
                                games = games.Where(g => g.GameModes.Count <= value);
                            }

                        }
                        else
                        {
                            return BadRequest("Item not found.");
                        }

                    }
                    else
                    {
                        if (splitItem[0] == "Name")
                        {
                            games = games.Where(g => EF.Functions.Like(g.Name, $"%{splitItem[1]}%")); //OR games.Where(g => g.Name.ToLower().Contains(splitItem[1].ToLower()));
                        }
                        else
                        {
                            return BadRequest("Item not found.");
                        }

                    }

                }
            }

            //sort
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
                .Include(g => g.GameModes).ThenInclude(gm => gm.winCondition)
                .SingleOrDefault(g => g.ID == id));
        }

        public class GameModeItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int WinConditionID { get; set; }
        }
        public class GameItem
        {
            public string Name { get; set; }
            public int MinPlayerCount { get; set; }
            public int MaxPlayerCount { get; set; }
            public List<GameModeItem> GameModeItems { get; set; }

        }


        [HttpPost]
        public IActionResult Post(GameItem item)
        {
            //if (!ModelState.IsValid)return BadRequest("Not a valid model");
            //if (string.IsNullOrEmpty(item.Name))return BadRequest("No name is provided");
            //if (item == null)return BadRequest("No data is provided");
            //if ID already exists?
            if (item.Name.Length < 1 || item.Name == null)
            {
                return BadRequest("Name has to be at least 1 letter.");
            }
            else if (item.MinPlayerCount == null || item.MaxPlayerCount == null)
            {
                return BadRequest("MinPlayerCount/MaxPlayerCount cannot be empty");
            }
            else if (item.MinPlayerCount > item.MaxPlayerCount)
            {
                return BadRequest("MinPlayerCount has to be smaller than MaxPlayerCount/MaxPlayerCount has to be larger than MinPlayerCount.");
            }
            else if (item.GameModeItems.Count == 0 || item.GameModeItems == null)
            {
                return BadRequest("Each game has to have at least one GameMode.");
            }
            else
            {
                var game = new Game
                {
                    Name = item.Name,
                    MinPlayerCount = item.MinPlayerCount,
                    MaxPlayerCount = item.MaxPlayerCount,
                    GameModes = new List<GameMode>()
                };

                _context.Games.Add(game);

                foreach (var gameMode in item.GameModeItems)
                {
                    if(_context.WinConditions.SingleOrDefault(w => w.ID == gameMode.WinConditionID) == null)
                    {
                        return BadRequest("Cannot find WinConditionId");
                    }
                    else
                    {
                        var gm = new GameMode
                        {
                            Name = gameMode.Name,
                            Description = gameMode.Description,
                            winCondition = _context.WinConditions.SingleOrDefault(w => w.ID == gameMode.WinConditionID)
                        };
                        game.GameModes.Add(gm);
                    }
                   
                }

            }

            _context.SaveChanges();

            return Ok(
                new
                {   game = _context.Games.OrderByDescending(g => g.ID).FirstOrDefault(),
                    message = "Item is posted"
                }) ;
        }

        public class EditGameModeItem
        {
            public int? ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int WinConditionID { get; set; }
        }

        public class EditItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int MinPlayerCount { get; set; }
            public int MaxPlayerCount { get; set; }
            public List<EditGameModeItem> EditGameModeItems { get; set; }
        }

        [HttpPost]
        [Route("editGame")]
        public ActionResult Edit(EditItem editItem)
        {
            var game = _context.Games
                .Include(g => g.GameModes)
                .Include(g => g.GameModes).ThenInclude(gm => gm.winCondition)
                .SingleOrDefault(g => g.ID == editItem.ID);

            if (editItem.Name.Length < 1 || editItem.Name == null)
            {
                return BadRequest("Name has to be at least 1 letter.");
            }

            else if (editItem.MinPlayerCount == null || editItem.MaxPlayerCount == null)
            {
                return BadRequest("MinPlayerCount/MaxPlayerCount cannot be empty");
            }
            else if (editItem.MinPlayerCount > editItem.MaxPlayerCount)
            {
                return BadRequest("MinPlayerCount has to be smaller than MaxPlayerCount/MaxPlayerCount has to be larger than MinPlayerCount.");
            }
            else
            {
                game.Name = editItem.Name;
                game.MinPlayerCount = editItem.MinPlayerCount;
                game.MaxPlayerCount = editItem.MaxPlayerCount;
            }

            if (editItem.EditGameModeItems.Count == 0 || editItem.EditGameModeItems == null)
            {
                return BadRequest("Each game has to have at least one GameMode.");
            }
            else
            {
                for (var i = 0; i < editItem.EditGameModeItems.Count; i++)
                {
                    var gm = game.GameModes.SingleOrDefault(g => g.ID == editItem.EditGameModeItems[i].ID);
                    if (gm != null)
                    {
                        gm.Name = editItem.EditGameModeItems[i].Name;
                        gm.Description = editItem.EditGameModeItems[i].Description;
                        gm.winCondition = _context.WinConditions.SingleOrDefault(w => w.ID == editItem.EditGameModeItems[i].WinConditionID);
                        _context.Update(gm);
                    }
                    else if (gm == null)
                    {
                        if(_context.WinConditions.SingleOrDefault(w => w.ID == editItem.EditGameModeItems[i].WinConditionID)==null)
                        {
                            return BadRequest("Cannot find WinConditionId");
                        }
                        else
                        {
                            var gamemode = new GameMode
                            {
                                Name = editItem.EditGameModeItems[i].Name,
                                Description = editItem.EditGameModeItems[i].Description,
                                winCondition = _context.WinConditions.SingleOrDefault(w => w.ID == editItem.EditGameModeItems[i].WinConditionID)
                            };
                            game.GameModes.Add(gamemode);
                        }
                       
                    }

                }
            }

            var originalGameModeList = _context.GameModes.Where(gm => gm.game.ID == editItem.ID).ToList();
            var deletedGameModes = originalGameModeList.Where(gm => editItem.EditGameModeItems.All(gm2 => gm2 != null && gm2.ID != gm.ID));
            if (deletedGameModes != null )
            {
                _context.GameModes.RemoveRange(deletedGameModes);
            }
            _context.Update(game);
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
                //var gm = _context.GameMatches.Where(g => g.game.ID == id);
                if (id == null)
                {
                    return BadRequest("Game not found.");

                }
                else if (!ModelState.IsValid)
                {
                    return BadRequest("Not a valid model.");
                }
                else if (_context.GameMatches.Where(g=>g.game.ID==id).Count() != 0)
                {
                    return BadRequest("Game " + id + " exists in Game Match(es).");
                }

                _context.Games.Remove(_context.Games.SingleOrDefault(g => g.ID == id));
                var gamemodes = _context.GameModes.Where(g => g.game.ID == id);
                foreach (var gm in gamemodes)
                {
                    _context.GameModes.Remove(gm);
                }
               
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
                    }else if (_context.GameMatches.Where(g => g.game.ID==id).Count()!=0)
                    {
                        return BadRequest("Game " + id + " exists in Game Match(es).");
                    }
                    else
                    {
                        _context.Games.Remove(item);
                    }
                    
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
