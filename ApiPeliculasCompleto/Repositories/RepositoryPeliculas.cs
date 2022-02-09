using ApiPeliculasCompleto.Data;
using ApiPeliculasCompleto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Repositories
{
    public class RepositoryPeliculas
    {
        PeliculasContext context;

        public RepositoryPeliculas(PeliculasContext context) {
            this.context = context;
        }

        public List<Genero> GetGeneros() {
            return this.context.Generos.ToList();
        }

        public List<Pelicula> GetPeliculas() {
            return this.context.Peliculas.ToList();
        }

        public List<Pelicula> GetPeliculasGenero(int idgenero) {
            var consulta = from datos in this.context.Peliculas
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }

        public Pelicula FindPelicula(int id) {
            return this.context.Peliculas.SingleOrDefault(x => x.IdPelicula == id);
        }

        public Cliente FindCliente(int id) {
            return this.context.Clientes.Single(x => x.IdCliente == id);
        }

        public List<PedidosCliente> GetPedidosCliente(int id) {
            var consulta = from datos in this.context.PedidosClientes
                           where datos.IdCliente == id
                           select datos;
            return consulta.ToList();
        }

        // Para inicio de sesión
        public Cliente ExisteCliente(String email, int idcliente) {
            var consulta = from datos in this.context.Clientes
                           where datos.Email == email && datos.IdCliente == idcliente
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void AddPedido(int idcliente, int idpelicula, int cantidad, DateTime fecha, int precio) {
            Pedido pedido = new Pedido();
            pedido.IdCliente = idcliente;
            pedido.IdPelicula = idpelicula;
            pedido.Cantidad = cantidad;
            pedido.Fecha = fecha;
            pedido.Precio = precio;
            this.context.Pedidos.Add(pedido);
            this.context.SaveChanges();
        }

    }
}
