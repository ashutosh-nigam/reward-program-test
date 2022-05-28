using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RewardProgramAPI.Extras;
/// <summary>
/// Swagger Options for multiple api versions.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }
    /// <summary>
    /// Adding different versions
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        // add swagger document for every API version discovered
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }
    }
    /// <summary>
    /// Implementation of default configurations method
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }
    /// <summary>
    /// Get API Information
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = "Reward Program API. Stellar It Solutions",
            Description = "Stellar It Solutions - Full Stack Developer Coding Assessment",
            Contact = new OpenApiContact()
            {
                Name = "Ashutosh Nigam",
                Email = "mrashutoshnigam@gmail.com",
                Url = new Uri("https://www.ashutoshnigam.in")
            }
        };
        return info;
    }
    
}