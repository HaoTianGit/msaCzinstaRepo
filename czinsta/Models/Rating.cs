using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public bool Grading { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; } = default!;

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; } = default!;
    }
}
