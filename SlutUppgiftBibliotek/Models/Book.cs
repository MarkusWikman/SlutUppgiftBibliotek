using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftBibliotek.Models
{
    internal class Book
    {
        public int Id { get; set; }
        [StringLength(13)]
        public bool IsAvailable { get; set; } = true;
        public string ISBN { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; } = new List<Author>();
        public int PublicationYear { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
    }
}