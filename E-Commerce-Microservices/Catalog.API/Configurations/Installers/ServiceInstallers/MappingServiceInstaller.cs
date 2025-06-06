﻿using Catalog.API.Mappings;
using Common.Attributes;

namespace Catalog.API.Configurations.Installers.ServiceInstallers;

[InstallerOrder(Order = 9)]
public class MappingServiceInstaller : IServiceInstaller
{
    public Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return Task.CompletedTask;
    }
}
