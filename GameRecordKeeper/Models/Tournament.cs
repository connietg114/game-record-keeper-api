using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRecordKeeper.Models
{
    public class Tournament
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentType tournamentType{ get; set; }
    }
}
