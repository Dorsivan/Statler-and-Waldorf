using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StatlerAndWaldorf.Models
{
    public class Users
    {
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

        public DateTime createdAt = DateTime.Now;

        public DateTime lastSeen = DateTime.Now;
    }
}
