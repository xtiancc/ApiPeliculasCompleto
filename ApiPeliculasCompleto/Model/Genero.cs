using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Model
{
    [Table("GENEROS")]
    public class Genero
    {
        [Key]
        [Column("IDGENERO")]
        public int IdGenero { get; set; }
        [Column("GENERO")]
        public String NombreGenero { get; set; }
    }
}
