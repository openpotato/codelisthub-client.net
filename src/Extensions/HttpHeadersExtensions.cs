#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License. 
 */
#endregion

using System.Linq;
using System.Net.Http.Headers;

namespace System;

/// <summary>
/// Extensions for <see cref="HttpHeaders"/>
/// </summary>
public static class HttpHeadersExtensions
{
    /// <summary>
    /// Returns the first header value for a specified header as <see cref="int"/>
    /// </summary>
    /// <param name="httpHeaders">A <see cref="HttpHeaders"/> instance</param>
    /// <param name="name">ame of the header</param>
    /// <returns>A <see cref="int"/> value</returns>
    public static int GetFirstIntegerOrDefault(this HttpHeaders httpHeaders, string name)
    {
        var value = httpHeaders.GetValues(name).FirstOrDefault();
        if (value != null)
        {
            return int.Parse(value);
        }
        return default;
    }
}