// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringTemplateTests.cs" company="Allors bvba">
//   Copyright 2002-2013 Allors bvba.
// 
// Dual Licensed under
//   a) the General Public Licence v3 (GPL)
//   b) the Allors License
// 
// The GPL License is included in the file gpl.txt.
// The Allors License is an addendum to your contract.
// 
// Allors Applications is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// For more information visit http://www.allors.com/legal
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Allors.Domain
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class StringTemplateTests : DomainTest
    {
        private const int NrOfRuns = 100;

        [Test]
        public void WithObject()
        {

            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").Build();

            var stringTemplate = new StringTemplateBuilder(this.DatabaseSession).WithBody(
@"main(this) ::= <<
Hello $this.FirstName$!
>>").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", person } });
                Assert.AreEqual("Hello John!", result);
            }
        }

        [Test]
        public void Cache()
        {
            var person = new PersonBuilder(this.DatabaseSession).WithFirstName("John").Build();

            var stringTemplate = new StringTemplateBuilder(this.DatabaseSession).WithBody(
@"main(this) ::= <<
Hello $this.FirstName$!
>>").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", person } });
                Assert.AreEqual("Hello John!", result);
            }

            stringTemplate.Body = 
@"main(this) ::= <<
Hi $this.FirstName$!
>>";

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", person } });
                Assert.AreEqual("Hi John!", result);
            }
        }

        [Test]
        public void WithObjects()
        {
            var john = new PersonBuilder(this.DatabaseSession).WithFirstName("John").Build();
            var jane = new PersonBuilder(this.DatabaseSession).WithFirstName("Jane").Build();

            var objects = new Dictionary<string, IObject> { { "john", john }, { "jane", jane } };

            var stringTemplate =
                new StringTemplateBuilder(this.DatabaseSession).WithBody(
                    @"main(this) ::= <<
Hello $this.john.FirstName$ and $this.jane.FirstName$!
>>").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", objects } });
                Assert.AreEqual("Hello John and Jane!", result);
            }
        }

        [Test]
        public void WithEscapes()
        {
            var john = new PersonBuilder(this.DatabaseSession).WithFirstName("John").Build();
            var jane = new PersonBuilder(this.DatabaseSession).WithFirstName("Jane").Build();

            var objects = new Dictionary<string, IObject> { { "john", john }, { "jane", jane } };

            var stringTemplate =
                new StringTemplateBuilder(this.DatabaseSession).WithBody(
                    @"main(this) ::= <<
Hello $this.john.FirstName$ \$\\ $this.jane.FirstName$!
>>").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", objects } });
                Assert.AreEqual(@"Hello John $\ Jane!", result);
            }
        }

        [Test]
        [ExpectedException]
        public void InvalidGroup()
        {
            var stringTemplate = new StringTemplateBuilder(this.DatabaseSession).WithBody(
@"Invalid!!!").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                stringTemplate.Apply(new Dictionary<string, object> { { "this", string.Empty } });
            }
        }

        [Test]
        [ExpectedException]
        public void ErrorInTemplate()
        {
            var stringTemplate = new StringTemplateBuilder(this.DatabaseSession).WithBody(
@"main(this) ::= <<
Hello $this.FirstName$!
>>").Build();

            for (var i = 0; i < NrOfRuns; i++)
            {
                var result = stringTemplate.Apply(new Dictionary<string, object> { { "this", null } });
                Assert.AreEqual("Hello John!", result);
            }
        }
    }
}