using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    [Table("Locacoes")]
    public class Rent
    {
        public Rent()
        {
            RentMovieList = new List<RentMovie>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        public DateTime DateRent { get; set; }

        /// <summary>
        /// Relacionamento de muitos p/ muitos entre locaçãos e filmes
        /// </summary>
        public ICollection<RentMovie> RentMovieList { get; set; }
    }
}
