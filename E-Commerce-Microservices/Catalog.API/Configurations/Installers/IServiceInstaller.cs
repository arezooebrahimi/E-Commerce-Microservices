﻿namespace Catalog.API.Configurations.Installers;

public interface IServiceInstaller
{
    Task Install(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment);
}
