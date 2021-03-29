using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogSimpleMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Post Post { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        public string Content { get; set; }
        public Comment Parent { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual IEnumerable<Comment> Replies { get; set; }
    }
}
