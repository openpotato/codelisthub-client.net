#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;


namespace CodeListHub.Client
{
    /// <summary>
    /// A factory for <see cref="IRestClient"/> instances
    /// </summary>
    public static class RestClientFactory
    {
        /// <summary>
        /// Creates a new <see cref="IRestClient"/> instance
        /// </summary>
        /// <returns>A new <see cref="IRestClient"/> instance</returns>
        public static IRestClient CreateRestClient()
        {
            return CreateRestClientWithAcceptRequestHeader(MediaTypeNames.Application.Json);
        }

        /// <summary>
        /// Creates a new <see cref="IRestClient"/> instance
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns>A new <see cref="IRestClient"/> instance</returns>
        public static IRestClient CreateRestClientWithAcceptRequestHeader(string mediaType)
        {
            // Create dependency injection container
            var serviceCollection = new ServiceCollection();

            // Register Http Client
            serviceCollection
                .AddHttpClient<IRestClient, RestClient>(client =>
                {
                    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(AssemblyInfo.GetAgentName(), AssemblyInfo.GetVersion()));
                })
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.RequestTimeout)
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.ServiceUnavailable)
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            // Create IRestClient implementation
            var services = serviceCollection.BuildServiceProvider();

            // Return back IRestClient implementation
            return services.GetRequiredService<IRestClient>();
        }
    }
}
