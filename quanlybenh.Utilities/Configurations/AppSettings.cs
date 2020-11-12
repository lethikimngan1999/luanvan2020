using System;
using System.Configuration;
using System.Web.Configuration;



namespace quanlybenh.Utilities.Configurations
{
    public static class AppSettings
    {
        public static string ConnectString
        {
            get { return WebConfigurationManager.AppSettings["ConnectionStrings"]; }
        }

        public static string JwtKey
        {
            get { return WebConfigurationManager.AppSettings["JwtKey"]; }
        }
        public static string JwtIssuer
        {
            get { return WebConfigurationManager.AppSettings["JwtIssuer"]; }
        }
        public static string JwtExpireDays
        {
            get { return WebConfigurationManager.AppSettings["JwtExpireDays"]; }
        }
        public static string JwtFormatAudienceId
        {
            get { return WebConfigurationManager.AppSettings["JwtFormatAudienceId"]; }
        }

        public static string JwtFormatAudienceSecret
        {
            get { return WebConfigurationManager.AppSettings["JwtFormatAudienceSecret"]; }
        }

        public static string ApiDomain
        {
            get { return WebConfigurationManager.AppSettings["wn:ApiDomain"]; }
        }

        public static string DefaultPassword
        {
            get { return ConfigurationManager.AppSettings["DefaultPassword"]; }
        }

    }


}
