#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client 
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License. 
 */
#endregion

using System;
using System.Net.Http;

namespace CodeListHub.Client;

/// <summary>
/// An abstract base class for <see cref="ApiClient"/>
/// </summary>
public abstract class ApiBaseClient
{
    private readonly Uri _baseUrl;
    private readonly IRestClient _restClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiBaseClient"/> class.
    /// </summary>
    /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    public ApiBaseClient(HttpClient httpClient, string baseUrl)
    {
        _restClient = new RestClient(httpClient);
        _baseUrl = new Uri(baseUrl);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiBaseClient"/> class.
    /// </summary>
    /// <param name="restClient">An implementation of <see cref="IRestClient"/></param>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    public ApiBaseClient(IRestClient restClient, string baseUrl)
    {
        _restClient = restClient;
        _baseUrl = new Uri(baseUrl);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiBaseClient"/> class.
    /// </summary>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    public ApiBaseClient(string baseUrl)
        : this(RestClientFactory.CreateRestClient(), baseUrl)
    {
    }

    /// <summary>
    /// Creates an uri builder with the internal base url as strating point
    /// </summary>
    /// <returns>A new <see cref="UriBuilder"/> instance</returns>
    protected UriBuilder CreateUriBuilder()
    {
        return new UriBuilder(_baseUrl);
    }

    /// <summary>
    /// Gives back the interanal instance of the <see cref="IRestClient"/> implmentation
    /// </summary>
    /// <returns>A new <see cref="IRestClient"/> implementation</returns>
    protected IRestClient GetRestClient()
    {
        return _restClient;
    }
}
