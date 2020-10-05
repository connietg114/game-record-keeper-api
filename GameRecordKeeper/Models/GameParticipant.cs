using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRecordKeeper.Models
{
    public class GameParticipant
    {
        public int ID { get; set; }
        public virtual GameMatch gameMatch { get; set; }
        public string DisplayName { get; set; }
        public int RegisteredPlayer { get; set; }
    }
}
