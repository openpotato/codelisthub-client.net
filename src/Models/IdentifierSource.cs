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

namespace CodeListHub.Client
{
    /// <summary>
    /// The source of a general identifier
    /// </summary>
    public class IdentifierSource
    {
        /// <summary>
        /// Human-readable name of the source.
        /// </summary>
        public string LongName { get; set; }

        /// <summary>
        /// Short name of the source.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Url with further information.
        /// </summary>
        public Uri Url { get; set; }
    }
}
