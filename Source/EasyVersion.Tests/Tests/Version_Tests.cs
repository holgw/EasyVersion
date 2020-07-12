using System;
using EasyVersion.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = EasyVersion.Core.Version;

namespace EasyVersion.Tests
{
    [TestClass]
    public class MyVersion_Tests
    {
        [TestMethod]
        public void CTOR_Test()
        {
            // ACT
            var v = new Version(4, 8, 15);

            // ASSeRT
            Assert.IsTrue(v.Major == 4);
            Assert.IsTrue(v.Minor == 8);
            Assert.IsTrue(v.Patch == 15);
        }

        [TestMethod]
        public void IncrementPatch_Test()
        {
            // ARRANGE
            var v = new Version(2, 15, 6);

            // ACT
            v.IncrementPatch();

            // ASSeRT
            Assert.IsTrue(v.Patch == 7);
            Assert.IsTrue(v.Minor == 15);
            Assert.IsTrue(v.Major == 2);
        }

        [TestMethod]
        public void IncrementMinor_Test()
        {
            // ARRANGE
            var v = new Version(2, 15, 6);

            // ACT
            v.IncrementMinor();

            // ASSeRT
            Assert.IsTrue(v.Patch == 0);
            Assert.IsTrue(v.Minor == 16);
            Assert.IsTrue(v.Major == 2);
        }

        [TestMethod]
        public void IncrementMajor_Test()
        {
            // ARRANGE
            var v = new Version(2, 15, 6);

            // ACT
            v.IncrementMajor();

            // ASSeRT
            Assert.IsTrue(v.Patch == 0);
            Assert.IsTrue(v.Minor == 0);
            Assert.IsTrue(v.Major == 3);
        }
    }
}
