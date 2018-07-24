using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using StatlerAndWaldorf.Models;

namespace StatlerAndWaldorf.DTO
{
    public class CreateReviewDTO
    {
        public string userId { get; set; }

        public string movieId { get; set; }

        public string review { get; set; }
    }
}
