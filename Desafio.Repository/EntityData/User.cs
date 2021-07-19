using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    [Table("Usuarios")]
    public class User
    {
        [Key]
        public int ID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(140)]
        public string Email { get; set; }

        [Required]
        public int Type { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime? Last_acess { get; set; }

    }
}
