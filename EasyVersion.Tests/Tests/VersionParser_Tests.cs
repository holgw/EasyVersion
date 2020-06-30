using System;
using EasyVersion.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = EasyVersion.Core.Version;

namespace EasyVersion.Tests.Tests
{
    [TestClass]
    public class VersionFilesParser_Tests
    {
        [TestMethod]
        public void Parse_Test1()
        {
            // ARRANGE
            var parser = new VersionParser();

            // ACT
            var v = parser.ParseVersion("12.0001.19");

            // ASSERT
            Assert.IsTrue(v.ToString() == "12.1.19.0");
        }

        [TestMethod]
        public void Parse_Test2()
        {
            // ARRANGE
            var parser = new VersionParser();

            // ACT
            var v = parser.ParseVersion("12.xxx.19");

            // ASSERT
            Assert.IsTrue(v.ToString() == "12.0.19.0");
        }

        [TestMethod]
        public void Parse_Test3()
        {
            // ARRANGE
            var parser = new VersionParser();

            // ACT
            var v = parser.ParseVersion("12.xxx");

            // ASSERT
            Assert.IsTrue(v.ToString() == "12.0.0.0");
        }

        [TestMethod]
        public void Parse_Test4()
        {
            // ARRANGE
            var parser = new VersionParser();

            // ACT
            var v = parser.ParseVersion(".xxx");

            // ASSERT
            Assert.IsTrue(v.ToString() == "0.0.0.0");
        }
    }
}
