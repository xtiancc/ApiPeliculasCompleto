using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Model
{
    [Table("PEDIDOS")]
    public class Pedido
    {
        [Column("IDCLIENTE")]
        public int IdCliente { get; set; }
        [Column("IDPELICULA")]
        public int IdPelicula { get; set; }
        [Column("CANTIDAD")]
        public int Cantidad { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("PRECIO")]
        public int Precio { get; set; }


    }
}
