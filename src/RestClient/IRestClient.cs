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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CodeListHub.Client;

/// <summary>
/// A typed HTTP client interface
/// </summary>
public interface IRestClient
{
    /// <summary>
    /// Request an API endpoint and return back a list of elements
    /// </summary>
    /// <typeparam name="T">The type of the element to be returned</typeparam>
    /// <param name="requestUrl">The request url</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains the list of elements.</returns>
    Task<IReadOnlyList<T>> GetListAsync<T>(Uri requestUrl, CancellationToken cancellationToken) where T : class;

    /// <summary>
    /// Request an API endpoint and return back a page of elements
    /// </summary>
    /// <typeparam name="T">The type of the element to be returned</typeparam>
    /// <param name="requestUrl">The request url</param>
    /// <param name="nextPage">A delegate for the getting the next page.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains the page of elements.</returns>
    Task<IReadOnlyPagedCollection<T>> GetPageAsync<T>(Uri requestUrl, Func<CancellationToken, Task<IReadOnlyPagedCollection<T>>> nextPage, CancellationToken cancellationToken) where T : class;

    /// <summary>
    /// Request an API endpoint and return back the raw response stream
    /// </summary>
    /// <param name="requestUrl">The request url</param>
    /// <param name="mediaType">The request media type</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains the stream.</returns>
    Task<Stream> GetStreamAsync(Uri requestUrl, string mediaType, CancellationToken cancellationToken);
}
