using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi.Models
{
    public class Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MinPlayerCount { get; set; }
        public int MaxPlayerCount { get; set; }
        public List<GameMode> GameModes { get; set; }
    }
}
