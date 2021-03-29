using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogSimpleMVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Published { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
