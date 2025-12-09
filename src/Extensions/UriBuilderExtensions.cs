#region CodeListHub .NET Client - Copyright (c) STÜBER SYSTEMS GmbH
/*    
 *    CodeListHub .NET Client
 *    
 *    Copyright (c) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 */
#endregion

using System.Globalization;
using System.Text;

namespace System
{
    /// <summary>
    /// Extensions for <see cref="UriBuilder"/>
    /// </summary>
    public static class UriBuilderExtensions
    {
        /// <summary>
        /// Appends a query string parameter with a key, and an integer value. 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, int value)
        {
            return ub.WithParameter(key, value.ToString());
        }

        /// <summary>
        /// Appends a query string parameter with a key, and a boolean value. 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, bool value)
        {
            return ub.WithParameter(key, value.ToString());
        }

        /// <summary>
        /// Appends a query string parameter with a key, and an array of string value. 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, string[] values)
        {
            return ub.WithParameter(key, values?.ToString());
        }

        /// <summary>
        /// Appends a query string parameter with a key, and an integer value. 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, DateTimeOffset? value)
        {
            return ub.WithParameter(key, value?.ToString("yyyy-MM-dd'T'HH:mm:ss.FFFK", DateTimeFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Appends a query string parameter with a key, and a value. 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder ub, string key, string value)
        {
            if (ub == null)
            {
                throw new ArgumentNullException(nameof(ub));
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                var sb = new StringBuilder();

                sb.Append(string.IsNullOrWhiteSpace(ub.Query) ? "" : $"{ub.Query.TrimStart('?')}&");
                sb.Append(Uri.EscapeDataString(key));
                sb.Append('=');
                sb.Append(Uri.EscapeDataString(value));

                ub.Query = sb.ToString();
            }
            return ub;
        }

        /// <summary>
        /// Appends a relative path 
        /// </summary>
        /// <param name="ub"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static UriBuilder WithRelativePath(this UriBuilder ub, string relativePath)
        {
            ub.Path += relativePath;
            return ub;
        }
    }
}