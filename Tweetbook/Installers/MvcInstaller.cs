using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "TweetBook API", Version = "v1" }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
