using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Model
{
    [Table("PELICULAS")]
    public class Pelicula
    {
        [Key]
        [Column("IDPELICULA")]
        public int IdPelicula { get; set; }
        [Column("IDGENERO")]
        public int IdGenero { get; set; }
        [Column("TITULO")]
        public String Titulo { get; set; }
        [Column("ARGUMENTO")]
        public String Argumento { get; set; }
        [Column("FOTO")]
        public String Foto { get; set; }
        [Column("FECHA_ESTRENO")]
        public DateTime Fecha { get; set; }

        [Column("ACTORES")]
        public String Actores { get; set; }

        [Column("PRECIO")]
        public int Precio { get; set; }

        [Column("YOUTUBE")]
        public String YouTube { get; set; }
    }
}
