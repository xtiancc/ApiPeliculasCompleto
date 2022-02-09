using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// En todos los proyectos va a ser igual
namespace ApiPeliculasCompleto.Helper
{
    public class HelperOAuthToken
    {
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String SecretKey { get; set; }

        public HelperOAuthToken(IConfiguration configuration)
        {
            this.Issuer = configuration.GetValue<String>("ApiOAuth:Issuer");
            this.Audience = configuration.GetValue<String>("ApiOAuth:Audience");
            this.SecretKey = configuration.GetValue<String>("ApiOAuth:SecretKey");
        }

        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = Encoding.UTF8.GetBytes(this.SecretKey);
            return new SymmetricSecurityKey(data);
        }

        public Action<JwtBearerOptions> GetJwtOptions()
        {
            Action<JwtBearerOptions> jwtoptions = new Action<JwtBearerOptions>(options =>
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateActor = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = this.Issuer,
                   ValidAudience = this.Audience,
                   IssuerSigningKey = this.GetKeyToken()
               }
            );
            return jwtoptions;
        }

        public Action<AuthenticationOptions> GetAuthOptions()
        {
            Action<AuthenticationOptions> authoptions = new Action<AuthenticationOptions>(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            return authoptions;
        }
    }
}
