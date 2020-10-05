using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRecordKeeper.Models
{
    public class GameMatch
    {
        public int ID { get; set; }
        public DateTime MatchDate { get; set; }
        public Game game { get; set; }
        public Tournament tournament { get; set; }
    }
}
