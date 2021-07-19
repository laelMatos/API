using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Repository
{
    public class RentMovie
    {
        [ForeignKey("Rent")]
        public int RentId { get; set; }
        public Rent Rent { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}