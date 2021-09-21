using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Caption { get; set; } = default!;

        [Required]
        public string PostImage { get; set; } = default!;

        public int LikeNumber { get; set; }

        public int DislikeNumber { get; set; }

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; } = default!;

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
