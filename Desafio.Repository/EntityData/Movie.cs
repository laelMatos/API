using Desafio.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    [Table("Filmes")]
    public class Movie
    {
        public Movie()
        {
            this.GenreMovies = new List<GenreMovie>();
            this.RentMovies = new List<RentMovie>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool Active { get; set; }

        [NotMapped]
        public eStatus eActive
        {
            get { return (eStatus)(this.Active?1:0); }
            set { this.Active = (value != 0); }
        }

        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }


        /// <summary>
        /// Relacionamento de muitos p/ muitos entre filmes e gêneros
        /// </summary>
        public ICollection<GenreMovie> GenreMovies { get; set; }

        /// <summary>
        /// Relacionamento de muitos p/ muitos entre locaçãos e filmes
        /// </summary>
        public ICollection<RentMovie> RentMovies { get; set; }
    }
}
