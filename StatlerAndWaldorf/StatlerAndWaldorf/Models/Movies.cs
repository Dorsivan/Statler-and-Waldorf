using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace StatlerAndWaldorf.Models
{
    public class Movies
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        public int Length { get; set; }

        public byte[] Image { get; set; }

        public string ContentType { get; set; }
    }
}
