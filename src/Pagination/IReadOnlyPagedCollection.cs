#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License. 
 */
#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CodeListHub.Client;

/// <summary>
/// A <see cref="IReadOnlyList{T}"/> with additional pagination information
/// </summary>
/// <typeparam name="T">The type of the elements in the list</typeparam>
public interface IReadOnlyPagedCollection<T>: IReadOnlyList<T>
{
    /// <summary>
    /// The page index
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// The page size
    /// </summary>
    int PageSize { get; }

    /// <summary>
    /// The total number of elements
    /// </summary>
    int TotalCount { get; }

    /// <summary>
    /// The total number of pages
    /// </summary>
    int TotalPages { get; }

    /// <summary>
    /// Returns the next page with elements
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IReadOnlyCollection{T}"/> instance</returns>
    Task<IReadOnlyPagedCollection<T>> GetNextPageAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns whether this is the last page within a page collection
    /// </summary>
    /// <returns>TRUE, if this is the last page</returns>
    bool IsLastPage();
}
