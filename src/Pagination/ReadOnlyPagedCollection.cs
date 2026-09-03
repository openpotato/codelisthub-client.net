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
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace CodeListHub.Client;

/// <summary>
/// A <see cref="ReadOnlyCollection{T}"/> with additional pagination information
/// </summary>
/// <typeparam name="T">The type of the list elements</typeparam>
public class ReadOnlyPagedCollection<T>: ReadOnlyCollection<T>, IReadOnlyPagedCollection<T>
{
    private readonly Func<CancellationToken, Task<IReadOnlyPagedCollection<T>>> _nextPage;
    private readonly int _pageIndex;
    private readonly int _pageSize;
    private readonly int _totalCount;
    private readonly int _totalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyPagedCollection{T}"/> class.
    /// </summary>
    /// <param name="list">The elements for the internal list</param>
    /// <param name="pageIndex">The page index</param>
    /// <param name="pageSize">The page size</param>
    /// <param name="totalPages">The total number of pages</param>
    /// <param name="totalCount">The total number of elements</param>
    public ReadOnlyPagedCollection(IList<T> list, int pageIndex, int pageSize, int totalPages, int totalCount, 
        Func<CancellationToken, Task<IReadOnlyPagedCollection<T>>> nextPage)
        : base(list)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        _totalPages = totalPages;
        _totalCount = totalCount;
        _nextPage = nextPage;
    }

    /// <summary>
    /// The page index
    /// </summary>
    public int PageIndex
    {
        get
        {
            return _pageIndex;
        }
    }

    /// <summary>
    /// The page size
    /// </summary>
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
    }

    /// <summary>
    /// The total number of elements 
    /// </summary>
    public int TotalCount
    { 
        get 
        {
            return _totalCount;
        }
    }

    /// <summary>
    /// The total number of pages
    /// </summary>
    public int TotalPages 
    { 
        get 
        {
            return _totalPages;
        }
    }

    /// <summary>
    /// Returns the next page with elements
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A new <see cref="IReadOnlyCollection{T}"/> instance</returns>
    public async Task<IReadOnlyPagedCollection<T>> GetNextPageAsync(CancellationToken cancellationToken = default)
    {
        return IsLastPage() ? null : await _nextPage(cancellationToken);
    }

    /// <summary>
    /// Returns whether this is the last page
    /// </summary>
    /// <returns>TRUE, if this is the last page</returns>
    public bool IsLastPage()
    {
        return (_pageIndex * _pageSize) >= _totalCount;
    }
}
