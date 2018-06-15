using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerAndWaldorf.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public int userId { get; set; }

        public string review { get; set; }

        public bool isBlocked { get; set; }

        public DateTime timePosted;
    }
}
