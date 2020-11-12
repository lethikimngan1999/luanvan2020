using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using quanlybenh.Utilities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;

namespace quanlybenh.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            app.UseJwtBearerAuthentication(
              new JwtBearerAuthenticationOptions
              {
                  AuthenticationMode = AuthenticationMode.Active,
                  AllowedAudiences = new[] { AppSettings.JwtIssuer },
                  IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                  {
                       new SymmetricKeyIssuerSecurityKeyProvider( AppSettings.JwtIssuer, Encoding.UTF8.GetBytes(AppSettings.JwtKey))
                  }
              });

            app.UseWebApi(config);
        }
    }


}