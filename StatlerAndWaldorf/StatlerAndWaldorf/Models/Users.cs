using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StatlerAndWaldorf.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Unique]
        [MaxLength(100)]
        public string email { get; set; }

        public string passwordHash { get; set; }

        [MaxLength(25)]
        public string firstName { get; set; }

        [MaxLength(25)]
        public string lastName { get; set; }

        public bool admin { get; set; }

        public string country { get; set; } 

        public DateTime createdAt = DateTime.Now;

        public DateTime lastSeen = DateTime.Now;
    }
}
