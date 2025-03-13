using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Text;

[assembly: OwinStartup(typeof(MicroserviceCitas.App_Start.OwinConfig))]

namespace MicroserviceCitas.App_Start
{
    public class OwinConfig
    {
        public void Configuration(IAppBuilder app)
        {
            var secretKey = Encoding.ASCII.GetBytes("clave_secreta_para_dieciseis_caracteres");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false, // Configura si usas Identity Server
                    ValidateAudience = false // Configura si usas Identity Server
                }
            });
        }
    }
}