using ApiPeliculasCompleto.Helper;
using ApiPeliculasCompleto.Model;
using ApiPeliculasCompleto.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiPeliculasCompleto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        RepositoryPeliculas repo;
        HelperOAuthToken helper;

        public AuthController(RepositoryPeliculas repo, HelperOAuthToken helper) {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginModel model) {
            Cliente cliente = this.repo.ExisteCliente(model.Username, int.Parse(model.Password));
            
            if (cliente == null) return Unauthorized();
            else {
                Claim[] claims = new[] {
                    new Claim("UserData", JsonConvert.SerializeObject(cliente))
                };
                // Cómo va a estar formado nuestro token y duración
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
