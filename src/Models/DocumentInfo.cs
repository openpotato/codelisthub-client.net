#region CodeListHub - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub 
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using System;

namespace CodeListHub.Client;

/// <summary>
/// Representation of an entry in the document index
/// </summary>
public class DocumentInfo
{
    /// <summary>
    /// Canonical URI which uniquely identifies all versions this document (collectively)
    /// </summary>
    public Uri CanonicalUri { get; set; }

    /// <summary>
    /// Canonical URI which uniquely identifies this document
    /// </summary>
    public Uri CanonicalVersionUri { get; set; }

    /// <summary>
    /// The language of the document
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Human-readable name of this document
    /// </summary>
    public string LongName { get; set; }

    /// <summary>
    /// The timepoint of the publication of the document.
    /// </summary>
    public DateTimeOffset? PublishedAt { get; set; }

    /// <summary>
    /// The publisher that is responsible for publication and/or maintenance of the codes
    /// </summary>
    public Publisher Publisher { get; set; }

    /// <summary>
    /// A short identifier of this document
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// A list of tags for this document
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// The document type
    /// </summary>
    public DocumentType Type { get; set; }

    /// <summary>
    /// The timepoint from which this document is valid.
    /// </summary>
    public DateTimeOffset? ValidFrom { get; set; }

    /// <summary>
    /// The timepoint until which this document is valid.
    public DateTimeOffset? ValidTo { get; set; }

    /// <summary>
    /// The version of the document
    /// </summary>
    public string Version { get; set; }
}
