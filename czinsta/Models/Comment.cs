using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; } = default!;

        [Required]
        public int AccountId { get; set; }

        public Account Account { get; set; } = default!;

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
