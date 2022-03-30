using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models
{
    //This class includes all the fields found in the mySQL database for bowlers including the foreign key for teams
    public class Bowler
    {
        [Key]
        [Required]
        public int BowlerID { get; set; }
        [Required]
        public string BowlerLastName { get; set; }
        [Required]
        public string BowlerFirstName { get; set; }
        public string BowlerMiddleInit { get; set; }
        [Required]
        public string BowlerAddress { get; set; }
        [Required]     
        public string BowlerCity { get; set; }
        [Required]
        public string BowlerState { get; set; }
        [Required]
        public string BowlerZip { get; set; }      
        public string BowlerPhoneNumber { get; set; }
        [Required]
        public int TeamID { get; set; }
        public Team Team { get; set; }
    }
}
