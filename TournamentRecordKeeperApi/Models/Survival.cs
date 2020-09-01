using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi.Models
{
    public class Survival
    {
        public int ID { get; set; }
        public GameMode gameMode { get; set; }
        public int Threshold { get; set; }
    }
}
