using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TournamentRecordKeeperApi.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Gender { get; set; }
        public string PreferredName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}
