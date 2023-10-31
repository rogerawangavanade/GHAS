using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Security;
using webapi.Configurations;
using webapi.Services;

public class AzureAdServices : IAzureAdServices
{
    private readonly AzureAdConfig _azureAdConfig;

    public AzureAdServices(IOptionsMonitor<AzureAdConfig> azureAdConfig)
    {
        _azureAdConfig = azureAdConfig.CurrentValue;
    }

    public string GetClientId()
    {
        return _azureAdConfig.ClientId;
    }

    public string GetTenantId()
    {
        return _azureAdConfig.TenantId;
    }
}
