using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StatlerAndWaldorf.Models
{
    public class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int userId { get; set; }

        public int movieId { get; set; }

        public string review { get; set; }

        public bool isBlocked { get; set; }

        public DateTime timePosted;
    }
}
