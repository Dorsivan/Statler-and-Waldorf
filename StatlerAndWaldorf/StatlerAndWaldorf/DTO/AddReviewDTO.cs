using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StatlerAndWaldorf.DTO
{
    public class AddReviewDTO
    {
        public int Id { get; set; }

        public int userId { get; set; }

        public int movieId { get; set; }

        public string review { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime timePosted;
    }
}
