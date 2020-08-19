using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi.Models
{
    public class BestOf
    {
        public int ID { get; set; }
        public virtual GameMode gameMode { get; set; }
        public int Win { get; set; }
        public int NumberOfMatches { get; set; }
    }
}
