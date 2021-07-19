using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class GenreMovie
    {
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
