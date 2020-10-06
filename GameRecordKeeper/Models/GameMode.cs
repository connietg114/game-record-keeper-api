using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRecordKeeper.Models
{
    public class GameMode
    {
        public int ID { get; set; }
        public Game game { get; set; }
        public string Name { get; set; }
        public WinCondition winCondition { get; set; }

    }
}
