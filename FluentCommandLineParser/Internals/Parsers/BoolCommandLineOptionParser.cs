#region License
// BoolCommandLineOptionParser.cs
// Copyright (c) 2013, Simon Williams
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provide
// d that the following conditions are met:
// 
// Redistributions of source code must retain the above copyright notice, this list of conditions and the
// following disclaimer.
// 
// Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
// the following disclaimer in the documentation and/or other materials provided with the distribution.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.
#endregion

using Fclp.Internals.Extensions;

namespace Fclp.Internals.Parsers
{
    /// <summary>
    /// Parser used to convert to <see cref="System.Boolean"/>.
    /// </summary>
    /// <remarks>For <see cref="System.Boolean"/> types the value is optional. If no value is provided for the Option then <c>true</c> is returned.</remarks>
    public class BoolCommandLineOptionParser : ICommandLineOptionParser<bool>
    {
        /// <summary>
        /// Parses the specified <see cref="System.String"/> into a <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="value">The value to parse, or <c>null</c> if none available.</param>
        /// <returns>
        /// A <see cref="System.Boolean"/> representing the parsed value.
        /// The value is optional. If no value is provided then <c>true</c> is returned.
        /// </returns>
        public bool Parse(string value)
        {
            return value.IsNullOrWhiteSpace() || bool.Parse(value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="System.String"/> can be parsed by this <see cref="ICommandLineOptionParser{T}"/>; otherwise <c>false</c>.</returns>
        public bool CanParse(string value)
        {
            bool result;
            return value.IsNullOrWhiteSpace() || bool.TryParse(value, out result);
        }
    }
}