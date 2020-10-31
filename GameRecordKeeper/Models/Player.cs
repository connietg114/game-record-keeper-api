using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRecordKeeper.Models
{
    public enum Gender
    {
        NotKnown = 0,
        Male = 1,
        Female = 2,
        NotApplicable = 9
    }

    public class Player
    {
        public Player()
        {
            Gender = Gender.NotKnown;
        }

        public int ID { get; set; }
        public Gender Gender { get; set; }
        public string PreferredName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public ApplicationUser Account { get; set; }
    }
}
