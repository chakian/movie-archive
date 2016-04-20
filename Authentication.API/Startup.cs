﻿using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Authentication.API.Startup))]
namespace Authentication.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}