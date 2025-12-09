#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CodeListHub.Client
{
    /// <summary>
    /// The implementation of the <see cref="IRestClient"/> interface,
    /// </summary>
    /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
    public class RestClient(HttpClient httpClient) : IRestClient
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// Request an API endpoint and return back a list of elements
        /// </summary>
        /// <typeparam name="T">The type of the element to be returned</typeparam>
        /// <param name="requestUrl">The request url</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains the list of elements.</returns>
        public async Task<IReadOnlyList<T>> GetListAsync<T>(Uri requestUrl, CancellationToken cancellationToken)
            where T : class
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return new ReadOnlyCollection<T>(await response.Content.ReadFromJsonAsync<List<T>>(CreateJsonSerializerOptions(), cancellationToken));
        }

        /// <summary>
        /// Request an API endpoint and return back a page of elements
        /// </summary>
        /// <typeparam name="T">The type of the element to be returned</typeparam>
        /// <param name="requestUrl">The request url</param>
        /// <param name="nextPage">A delegate for the getting the next page.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains the page of elements.</returns>
        public async Task<IReadOnlyPagedCollection<T>> GetPageAsync<T>(Uri requestUrl, Func<CancellationToken, Task<IReadOnlyPagedCollection<T>>> nextPage, CancellationToken cancellationToken)
            where T : class
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return new ReadOnlyPagedCollection<T>(await response.Content.ReadFromJsonAsync<List<T>>(CreateJsonSerializerOptions(), cancellationToken),
                response.Headers.GetFirstIntegerOrDefault("x-page"),
                response.Headers.GetFirstIntegerOrDefault("x-page-size"),
                response.Headers.GetFirstIntegerOrDefault("x-total-pages"),
                response.Headers.GetFirstIntegerOrDefault("x-total-count"),
                nextPage
            );
        }

        /// <summary>
        /// Request an API endpoint and return back a response stream
        /// </summary>
        /// <param name="requestUrl">The request url</param>
        /// <param name="mediaType">The request media type</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
        /// contains stream.</returns>
        public async Task<Stream> GetStreamAsync(Uri requestUrl, string mediaType, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            var response = await _httpClient.SendAsync(request, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync(cancellationToken);
        }

        /// <summary>
        /// Options to be used with <see cref="JsonSerializer"/>.
        /// </summary>
        /// <returns>A new <see cref="JsonSerializerOptions"/> instance</returns>
        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }
}
