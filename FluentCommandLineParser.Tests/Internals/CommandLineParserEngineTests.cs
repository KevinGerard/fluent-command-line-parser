﻿#region License
// CommandLineParserEngineTests.cs
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

using System.Globalization;
using System.Linq;
using Fclp.Internals;
using NUnit.Framework;

namespace FluentCommandLineParser.Tests.Internals
{
    /// <summary>
    /// Tests for <see cref="CommandLineParserEngine"/>.
    /// </summary>
    [TestFixture]
    public class CommandLineParserEngineTests
    {
        #region Common

        static void AssertSingleKeyValueResult(string arg, string expectedKey, string expectedValue)
        {
            var keys = new[] {"-", "--", "/"};
            var engine = new CommandLineParserEngine().AsInterface();

            foreach (var key in keys)
            {
                string keyArg = key + arg;

                var actual = engine.Parse(new[] { keyArg });

                var kvp = actual.Single();
                Assert.AreEqual(expectedKey, kvp.Key);
                Assert.AreEqual(expectedValue, kvp.Value);
            }
        }

        #endregion

        [Test]
        public void Ensure_Specifying_Null_Args_Returns_Empty_Results()
        {
            var engine = new CommandLineParserEngine().AsInterface();
            var actual = engine.Parse(null);
            Assert.IsFalse(actual.Any());
        }

        [Test]
        public void Ensure_Specifying_Empty_Args_Returns_Empty_Results()
        {
            var engine = new CommandLineParserEngine().AsInterface();
            var actual = engine.Parse(new string[0]);
            Assert.IsFalse(actual.Any());
        }

        #region Boolean Options Tests

        [Test]
        public void Ensure_Can_Parse_Positive_Boolean_Option()
        {
            AssertSingleKeyValueResult("opt+", "opt", "True");
        }

        [Test]
        public void Ensure_Can_Parse_Negative_Boolean_Option()
        {
            AssertSingleKeyValueResult("opt-", "opt", "False");
        }

        [Test]
        public void Ensure_Can_Parse_Empty_Boolean_Option()
        {
            AssertSingleKeyValueResult("opt", "opt", null);
        }

        #endregion

        #region Required Value Tests

        [Test]
        public void Ensure_Can_Parse_Required_Value_Option()
        {
            AssertSingleKeyValueResult("opt=value", "opt", "value");
        }

        [Test]
        public void Ensure_Can_Parse_Required_Value_Option_With_Long_Text()
        {
            const string longText = @"my long ish text, with a 'few' chars !£$%^&*()_+=-|`¬[]{};'#:@~<>?\/.,";
            const string quotedLongText = @"" + longText + @"";
            AssertSingleKeyValueResult(string.Format(CultureInfo.InvariantCulture, "opt={0}", quotedLongText), "opt", longText);
        }

        [Test]
        public void Ensure_Can_Parse_Required_Value_Option_With_More_Than_One_Seperating_Char()
        {
            const string longText = "remember that 1+1 = 2 and 1+1 != 3";
            const string quotedLongText = @"" + longText + @"";
            AssertSingleKeyValueResult(string.Format(CultureInfo.InvariantCulture, "opt={0}", quotedLongText), "opt", longText);
        }

        #endregion 

        #region Optional Value Tests
        
        [Test]
        public void Ensure_Can_Parse_Optional_Value_Option()
        {
            AssertSingleKeyValueResult("opt:value", "opt", "value");
        }

        [Test]
        public void Ensure_Can_Parse_Optional_Value_Option_With_Long_Text()
        {
            const string longText = @"my long ish text, with a 'few' chars !£$%^&*()_+=-|`¬[]{};'#:@~<>?\/.,";
            const string quotedLongText = @"" + longText + @"";
            AssertSingleKeyValueResult(string.Format(CultureInfo.InvariantCulture, "opt:{0}", quotedLongText), "opt", longText);
        }

        [Test]
        public void Ensure_Can_Parse_Optional_Value_Option_With_More_Than_One_Seperating_Char()
        {
            const string longText = "a 1:1 relationship";
            const string quotedLongText = @"" + longText + @"";
            AssertSingleKeyValueResult(string.Format(CultureInfo.InvariantCulture, "opt:{0}", quotedLongText), "opt", longText);
        }
        #endregion
    }
}

