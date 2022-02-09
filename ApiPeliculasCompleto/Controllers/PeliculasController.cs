using ApiPeliculasCompleto.Model;
using ApiPeliculasCompleto.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        // SE RECOMIENDA HACER UNO POR CADA MODEL. MODIFICAR EN EL FUTURO
        RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo) {
            this.repo = repo;
        }

        [HttpGet]
        public List<Pelicula> GetPeliculas()
        {
            return this.repo.GetPeliculas();
        }

        [HttpGet("{id}")]
        public Pelicula FindPelicula(int id)
        {
            return this.repo.FindPelicula(id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<Genero> Generos()
        {
            return this.repo.GetGeneros();
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public List<PedidosCliente> PedidosCliente() {
            // Recuperamos los datos del cliente ya que no viene por ruta
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(json);
            return this.repo.GetPedidosCliente(cliente.IdCliente);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public Cliente PerfilCliente() {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(json);
            return cliente;
        }

        [HttpGet]
        [Route("[action]/{idgenero}")]
        public List<Pelicula> PeliculasGenero(int idgenero)
        {
            return this.repo.GetPeliculasGenero(idgenero);
        }

        [HttpGet]
        [Route("[action]/{idcliente}")]
        public Cliente Cliente(int idcliente)
        {
            return this.repo.FindCliente(idcliente);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public void AddPedido(Pedido pedido)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string json = claims.SingleOrDefault(x => x.Type == "UserData").Value;
            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(json);
            this.repo.AddPedido(cliente.IdCliente, pedido.IdPelicula, pedido.Cantidad, pedido.Fecha, pedido.Precio);
        }
    }
}
