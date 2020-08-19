using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi.Models
{
    public class FirstOf
    {
        public int ID { get; set; }
        public virtual GameMode gameMode { get; set; }
        public int Threshold { get; set; }
    }
}
