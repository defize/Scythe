using System;
using System.Linq;

using NUnit.Framework;

namespace Defize.Scythe.Test
{
    [TestFixture]
    public class RawArgumentsTest
    {
        [Test]
        public void FlagParsesCorrectly()
        {
            var raw = RawArguments.Parse(new [] {"/ab"});

            var flag = raw.HasFlag("ab");

            Assert.IsTrue(flag);
        }

        [Test]
        public void AbsentFlagReturnsFalse()
        {
            var raw = RawArguments.Parse(new[] { "/ab" });

            var flag = raw.HasFlag("cd");

            Assert.IsFalse(flag);
        }

        [Test]
        public void PresentNamedArgumentReturnsTrue()
        {
            var raw = RawArguments.Parse(new[] { "/ab=wombles" });

            string value;
            var exists = raw.TryGetNamedArgument("ab", out value);

            Assert.IsTrue(exists);
        }

        [Test]
        public void PresentNamedArgumentReturnsCorrectValue()
        {
            var raw = RawArguments.Parse(new[] { "/ab=wombles" });

            string value;
            raw.TryGetNamedArgument("ab", out value);

            Assert.AreEqual("wombles", value);
        }

        [Test]
        public void AbsentNamedArgumentReturnsFalse()
        {
            var raw = RawArguments.Parse(new[] { "/ab=wombles" });

            string value;
            var exists = raw.TryGetNamedArgument("cd", out value);

            Assert.IsFalse(exists);
        }
    }
}
