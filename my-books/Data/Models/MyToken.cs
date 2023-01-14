﻿using System.ComponentModel.DataAnnotations.Schema;

namespace my_books.Data.Models
{
    public class MyToken
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public string JwtId { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime DateExpire { get; set; }


        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
