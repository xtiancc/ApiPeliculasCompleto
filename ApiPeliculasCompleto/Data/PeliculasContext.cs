using ApiPeliculasCompleto.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Data
{
    public class PeliculasContext: DbContext
    {
        public PeliculasContext(DbContextOptions<PeliculasContext> options) : base(options) { }

        // Generamos una clave compuesta para la tabla pedido puesto que no podemos indicar dos [Key] en un mismo model
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Pedido>().HasKey(x => new { x.IdCliente, x.IdPelicula });
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidosCliente> PedidosClientes { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
