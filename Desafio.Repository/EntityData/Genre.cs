using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio.Repository
{
    [Table("Generos")]
    public class Genre
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; }

        public DateTime Create_at { get; set; }

        /// <summary>
        /// Relacionamento de muitos p/ muitos entre filmes e gêneros
        /// </summary>
        public ICollection<GenreMovie> GenreMovies { get; set; }
    }
}