#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License. 
 */
#endregion

using OpenCodeList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace CodeListHub.Client;

/// <summary>
/// Client for the API endpoint of CodeListHub
/// </summary>
public class ApiClient : ApiBaseClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">A <see cref="HttpClient"/> instance</param>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    public ApiClient(HttpClient httpClient, string baseUrl)
        : base(httpClient, baseUrl)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClient"/> class.
    /// </summary>
    /// <param name="restClient">An implementation of <see cref="IRestClient"/></param>
    /// <param name="baseUrl">The base url of the CodeListHub API</param>
    public ApiClient(IRestClient restClient, string baseUrl)
        : base(restClient, baseUrl)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiClient"/> class.
    /// </summary>
    /// <param name="baseUrl">The base url of the OpenPLZ API</param>
    public ApiClient(string baseUrl)
        : base(RestClientFactory.CreateRestClient(), baseUrl)
    {
    }

    /// <summary>
    /// Returns the content of a code list document or a code list set document in a format other than OpenCodeList as stream
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="mediaType"">The requested format as media type.</param>
    /// <param name="language"">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="Stream"/> instance.</returns>
    public async Task<Stream> GetAlternativeFormatAsStreamAsync(
        Uri canonicalUri,
        string mediaType,
        string language = null,
        CancellationToken cancellationToken = default)
    {
        return await GetAlternativeFormatAsStreamAsync(canonicalUri.ToString(), mediaType, language, cancellationToken);
    }

    /// <summary>
    /// Returns the content of a code list document or a code list set document in a format other than OpenCodeList as stream
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="mediaType"">The requested format as media type.</param>
    /// <param name="language"">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="Stream"/> instance.</returns>
    public async Task<Stream> GetAlternativeFormatAsStreamAsync(
        string canonicalUri, 
        string mediaType, 
        string language = null, 
        CancellationToken cancellationToken = default)
    {
        return await GetRestClient().GetStreamAsync(
            CreateUriBuilder()
                .WithRelativePath($"documents/{canonicalUri}/alternative-format")
                .WithParameter("language", language)
                .Uri,
            mediaType, cancellationToken);
    }

    /// <summary>
    /// Returns a code list document or a code list set document in OpenCodeList format as raw stream
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="metaOnly">TRUE, only a meta document is returned</param>
    /// <param name="language"">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="Stream"/> instance.</returns>
    public async Task<Stream> GetDocumentAsStreamAsync(
        Uri canonicalUri,
        bool metaOnly,
        string language = null,
        CancellationToken cancellationToken = default)
    {
        return await GetDocumentAsStreamAsync(canonicalUri.ToString(), metaOnly, language, cancellationToken);
    }

    /// <summary>
    /// Returns a code list document or a code list set document in OpenCodeList format as raw stream
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="metaOnly">TRUE, only a meta document is returned</param>
    /// <param name="language"">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="Stream"/> instance.</returns>
    public async Task<Stream> GetDocumentAsStreamAsync(
        string canonicalUri, 
        bool metaOnly, 
        string language = null, 
        CancellationToken cancellationToken = default)
    {
        return await GetRestClient().GetStreamAsync(
            CreateUriBuilder()
                .WithRelativePath($"documents/{canonicalUri}")
                .WithParameter("metaOnly", metaOnly)
                .WithParameter("language", language)
                .Uri,
            MediaTypeNames.Application.Json, cancellationToken);
    }

    /// <summary>
    /// Returns a code list document or a code list set document
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="metaOnly">TRUE, only a meta document is returned</param>
    /// <param name="language"">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="CodeListBase"/> instance.</returns>
    public async Task<CodeListBase> GetDocumentAsync(
        Uri canonicalUri,
        bool metaOnly,
        string language = null,
        CancellationToken cancellationToken = default)
    {
        return await GetDocumentAsync(canonicalUri.ToString(), metaOnly, language, cancellationToken);
    }

    /// <summary>
    /// Returns a code list document or a code list set document
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="metaOnly">TRUE, only a meta document is returned</param>
    /// <param name="language">The language of the document.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a <see cref="CodeListBase"/> instance.</returns>
    public async Task<CodeListBase> GetDocumentAsync(
        string canonicalUri, 
        bool metaOnly, 
        string language = null, 
        CancellationToken cancellationToken = default)
    {
        return await CodeListLoader.LoadAsync(await GetDocumentAsStreamAsync(canonicalUri, metaOnly, language, cancellationToken), cancellationToken);
    }

    /// <summary>
    /// Returns the index of available code list documents and a code list set documents
    /// </summary>
    /// <param name="type">Document type filter</param>
    /// <param name="searchTerm">Search term as regular expression</param>
    /// <param name="tags">Tags filter</param>
    /// <param name="language">Language filter</param>
    /// <param name="publishedFrom">Filter for timepoint of publication (from value)</param>
    /// <param name="publishedUntil">Filter for timepoint of publication (to value)</param>
    /// <param name="pageIndex">Page index for paging</param>
    /// <param name="pageSize">Page size for paging</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a paged list of <see cref="DocumentInfo"/> instances.</returns>
    public async Task<IReadOnlyPagedCollection<DocumentInfo>> GetDocumentIndexAsync(
        DocumentType type,
        string searchTerm,
        string[] tags, 
        string language,
        DateTimeOffset? publishedFrom = null,
        DateTimeOffset? publishedUntil = null,
        int pageIndex = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await GetRestClient().GetPageAsync(
            CreateUriBuilder()
                .WithRelativePath($"documents/index")
                .WithParameter("type", type.ToString())
                .WithParameter("searchTerm", searchTerm)
                .WithParameter("tags", tags)
                .WithParameter("language", language)
                .WithParameter("publishedFrom", publishedFrom)
                .WithParameter("publishedUntil", publishedUntil)
                .WithParameter("page", pageIndex)
                .WithParameter("pageSize", pageSize)
                .Uri,
            async (ct) => await GetDocumentIndexAsync(type, searchTerm, tags, language, publishedFrom, publishedUntil, pageIndex + 1, pageSize, ct), cancellationToken);
    }

    /// <summary>
    /// Returns the list of available translations for a code list document or a code list set document
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a list of strings.</returns>
    public async Task<IReadOnlyCollection<string>> GetDocumentLanguagesAsync(
        Uri canonicalUri,
        CancellationToken cancellationToken = default)
    {
        return await GetDocumentLanguagesAsync(canonicalUri.ToString(), cancellationToken);
    }

    /// <summary>
    /// Returns the list of available translations for a code list document or a code list set document
    /// </summary>
    /// <param name="canonicalUri">A canonical URI which uniquely identifies the version of the document</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a list of strings.</returns>
    public async Task<IReadOnlyCollection<string>> GetDocumentLanguagesAsync(
        string canonicalUri, 
        CancellationToken cancellationToken = default)
    {
        return await GetRestClient().GetListAsync<string>(
            CreateUriBuilder()
                .WithRelativePath($"documents/{canonicalUri}/languages")
                .Uri,
            cancellationToken);
    }

    /// <summary>
    /// Returns the list of available tags
    /// </summary>
    /// <param name="pageIndex">Page index for paging</param>
    /// <param name="pageSize">Page size for paging</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter 
    /// contains a paged list of strings.</returns>
    public async Task<IReadOnlyPagedCollection<string>> GetTagsAsync(
        int pageIndex = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        return await GetRestClient().GetPageAsync(
            CreateUriBuilder()
                .WithRelativePath("documents/tags")
                .WithParameter("page", pageIndex)
                .WithParameter("pageSize", pageSize)
                .Uri,
            async (ct) => await GetTagsAsync(pageIndex + 1, pageSize, ct), cancellationToken);
    }
}
