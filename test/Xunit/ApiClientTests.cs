#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using CodeListHub.Client;
using OpenCodeList;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodeListHub.CLient.Tests
{
    /// <summary>
    /// Unit tests for <see cref="ApiClient"/>.
    /// </summary>
    public class ApiClientTests
    {
        [Fact]
        public async Task TestGetDocument()
        {
            var client = ApiClientFactory.CreateApiClient();

            var document = await client.GetDocumentAsync(new Uri("urn:codelisthub:iso:countries:v1"), false);

            Assert.NotNull(document);
            Assert.Equal(new Uri("urn:codelisthub:iso:countries"), document.Identification.CanonicalUri);
            Assert.Equal(new Uri("urn:codelisthub:iso:countries:v1"), document.Identification.CanonicalVersionUri);
            Assert.Equal("de", document.Identification.Language);
            Assert.Equal("v1", document.Identification.Version);

            if (document is CodeListDocument codeListDocument)
            {
                Assert.Equal(6, codeListDocument.Columns.Count);
                Assert.Equal("alpha2Code", codeListDocument.Columns[0].Id);
                Assert.Equal("Alpha 2 Schlüssel", codeListDocument.Columns[0].Name);

                Assert.True(codeListDocument.Rows.Count > 0);
                Assert.Equal("AD", codeListDocument.Rows[0]["alpha2Code"]);
                Assert.Equal("Andorra", codeListDocument.Rows[0]["name"]);
            }
        }

        [Fact]
        public async Task TestGetDocumentIndex()
        {
            var client = ApiClientFactory.CreateApiClient();

            var documentIndex = await client.GetDocumentIndexAsync(
                DocumentType.CodeList, "ISO", null, null, null, null, 1, 20);

            Assert.Equal(1, documentIndex.PageIndex);
            Assert.Equal(20, documentIndex.PageSize);

            Assert.True(documentIndex.Count > 0);
            Assert.True(documentIndex.TotalPages >= 1);
            Assert.True(documentIndex.TotalCount >= 1);

            var existsCodeList = false;

            foreach (var documentInfo in documentIndex)
            {
                if (documentInfo.ShortName == "ISO.CountryCodeList")
                {
                    existsCodeList = true;

                    Assert.Equal(new Uri("urn:codelisthub:iso:countries"), documentInfo.CanonicalUri);
                    Assert.Equal(new Uri("urn:codelisthub:iso:countries:v1"), documentInfo.CanonicalVersionUri);
                    Assert.Equal("de", documentInfo.Language);
                    Assert.Equal("v1", documentInfo.Version);

                    break;
                }
            }

            Assert.True(existsCodeList);
        }
    }
}
