#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License. 
 */
#endregion

using System.Net.Http;

namespace CodeListHub.Client;

/// <summary>
/// An api client factory for the CodeListHub API client
/// </summary>
public static class ApiClientFactory
{
    /// <summary>
    /// The offical base url of the CodeListHub API
    /// </summary>
    public const string CodeListHubBaseUrl = "https://api.codelisthub.org/v1/";

    /// <summary>
    /// returns a new instance of <see cref="ApiClient" />
    /// </summary>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    /// <returns>Returns a new <see cref="ApiClient" /></returns>
    public static ApiClient CreateApiClient(string baseUrl = CodeListHubBaseUrl)
    {
        return new ApiClient(baseUrl);
    }

    /// <summary>
    /// returns a new instance of <see cref="ApiClient" />
    /// </summary>
    /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    /// <returns>Returns a new <see cref="ApiClient" /></returns>
    public static ApiClient CreateApiClient(HttpClient httpClient, string baseUrl = CodeListHubBaseUrl)
    {
        return new ApiClient(httpClient, baseUrl);
    }
}
