using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Intern.Entities
{
    [Table("VoteStars")]
    public class VoteStar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VoteStarId { get; set; } 
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int StarVoted { get; set; }

        public Account Account { get; set; }
        public Product Product { get; set; }
    }
}
