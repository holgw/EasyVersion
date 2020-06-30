using System;
using EasyVersion.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = EasyVersion.Core.Version;

namespace EasyVersion.Tests
{
    [TestClass]
    public class AssemblyInfoContentParser_Tests
    {
        [TestMethod]
        public void ParseVersion_Test1()
        {
            // ARRANGE
            string[] rows = new string[]
            {
                "[assembly: System.Reflection.AssemblyTitle(\"My Assembly Title\")]",
                "[assembly: System.Reflection.AssemblyDescription(\"My Assembly Description\")]",
                "[assembly: System.Reflection.AssemblyCompany(\"My Company Name\")]",
                "[assembly: System.Reflection.AssemblyProduct(\"My Product\")]",
                "[assembly: System.Reflection.AssemblyCopyright(\"COPYRIGHT © 2020\")]",
                "[assembly: System.Runtime.InteropServices.ComVisible(false)]",
                "[assembly: System.Runtime.InteropServices.Guid(\"BE627789-D76A-409A-A69E-347E27C62627\")]",
                "[assembly: System.Reflection.AssemblyVersion(\"0.0\")]",
                "[assembly: System.Reflection.AssemblyFileVersion(\"4.8.15.16\")]",
            };
            string content = String.Join(Environment.NewLine, rows);
            var versionParser = new VersionParser();
            var contentParser = new AssemblyInfoContentParser(versionParser);

            // ACT
            var v = contentParser.ParseVersion(content);

            // ASSERT
            Assert.IsTrue(v.Major == 4);
            Assert.IsTrue(v.Minor == 8);
            Assert.IsTrue(v.Patch == 15);
        }

        [TestMethod]
        public void SetVersion_Test1()
        {
            // ARRANGE
            string[] rows = new string[]
            {
                "[assembly: System.Reflection.AssemblyTitle(\"My Assembly Title\")]",
                "[assembly: System.Reflection.AssemblyDescription(\"My Assembly Description\")]",
                "[assembly: System.Reflection.AssemblyCompany(\"My Comany Name\")]",
                "[assembly: System.Reflection.AssemblyProduct(\"My Product\")]",
                "[assembly: System.Reflection.AssemblyCopyright(\"COPYRIGHT © 2020\")]",
                "[assembly: System.Runtime.InteropServices.ComVisible(false)]",
                "[assembly: System.Runtime.InteropServices.Guid(\"BE627789-D76A-409A-A69E-347E27C62627\")]",
                "[assembly: System.Reflection.AssemblyVersion(\"0.0\")]",
                "[assembly: System.Reflection.AssemblyFileVersion(\"4.8.15.16\")]",
            };
            string content = String.Join(Environment.NewLine, rows);
            var versionParser = new VersionParser();
            var contentParser = new AssemblyInfoContentParser(versionParser);

            // ACT
            var v = new Version(16, 23, 42);
            string result = contentParser.SetVersion(content, v);

            // ASSERT
            string[] newRows = new string[]
            {
                "[assembly: System.Reflection.AssemblyTitle(\"My Assembly Title\")]",
                "[assembly: System.Reflection.AssemblyDescription(\"My Assembly Description\")]",
                "[assembly: System.Reflection.AssemblyCompany(\"My Comany Name\")]",
                "[assembly: System.Reflection.AssemblyProduct(\"My Product\")]",
                "[assembly: System.Reflection.AssemblyCopyright(\"COPYRIGHT © 2020\")]",
                "[assembly: System.Runtime.InteropServices.ComVisible(false)]",
                "[assembly: System.Runtime.InteropServices.Guid(\"BE627789-D76A-409A-A69E-347E27C62627\")]",
                "[assembly: System.Reflection.AssemblyVersion(\"0.0\")]",
                "[assembly: System.Reflection.AssemblyFileVersion(\"16.23.42.0\")]",
            };
            string newContent = String.Join(Environment.NewLine, newRows);
            v = contentParser.ParseVersion(newContent);
            Assert.IsTrue(v.Major == 16);
            Assert.IsTrue(v.Minor == 23);
            Assert.IsTrue(v.Patch == 42);
        }

        [TestMethod]
        public void SetVersion_Test2()
        {
            // ARRANGE
            string[] rows = new string[]
            {
                "[assembly: System.Reflection.AssemblyTitle(\"My Assembly Title\")]",
                "[assembly: System.Reflection.AssemblyDescription(\"My Assembly Description\")]",
                "[assembly: System.Reflection.AssemblyCompany(\"My Company Name\")]",
                "[assembly: System.Reflection.AssemblyProduct(\"My Product\")]",
                "[assembly: System.Reflection.AssemblyCopyright(\"COPYRIGHT © 2020\")]",
                "[assembly: System.Runtime.InteropServices.ComVisible(false)]",
                "[assembly: System.Runtime.InteropServices.Guid(\"BE627789-D76A-409A-A69E-347E27C62627\")]",
                "[assembly: System.Reflection.AssemblyVersion(\"0.0\")]",
                "[assembly: System.Reflection.AssemblyFileVersion(\"4.8.15.16\")]",
            };
            string content = String.Join(Environment.NewLine, rows);
            var versionParser = new VersionParser();
            var contentParser = new AssemblyInfoContentParser(versionParser);

            // ACT
            var v = new Version(16, 23, 42);
            string result = contentParser.SetVersion(content, v);

            // ASSERT
            string[] newRows = new string[]
            {
                "[assembly: System.Reflection.AssemblyTitle(\"My Assembly Title\")]",
                "[assembly: System.Reflection.AssemblyDescription(\"My Assembly Description\")]",
                "[assembly: System.Reflection.AssemblyCompany(\"My Company Name\")]",
                "[assembly: System.Reflection.AssemblyProduct(\"My Product\")]",
                "[assembly: System.Reflection.AssemblyCopyright(\"COPYRIGHT © 2020\")]",
                "[assembly: System.Runtime.InteropServices.ComVisible(false)]",
                "[assembly: System.Runtime.InteropServices.Guid(\"BE627789-D76A-409A-A69E-347E27C62627\")]",
                "[assembly: System.Reflection.AssemblyVersion(\"0.0\")]",
                "[assembly: System.Reflection.AssemblyFileVersion(\"16.23.42.0\")]",
            };
            string newContent = String.Join(Environment.NewLine, newRows);
            Assert.IsTrue(result == newContent);
        }

        [TestMethod]
        public void SetVersion_Test3()
        {
            // ARRANGE
            string[] rows = new string[]
            {
                    "Sed ut perspiciatis unde omnis iste natus error sit voluptatem",
                    "accusantium doloremque laudantium, totam rem aperiam, eaque ipsa",
                    "quae ab illo inventore veritatis et quasi architecto beatae vitae",
                    "dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit",
                    "aspernatur aut odit aut fugit, sed quia consequuntur magni dolores",
                    "eos qui ratione voluptatem sequi nesciunt."
            };
            string content = String.Join(Environment.NewLine, rows);
            var versionParser = new VersionParser();
            var contentParser = new AssemblyInfoContentParser(versionParser);

            // ACT
            var v = new Version(16, 23, 42);
            var ret = contentParser.SetVersion(content, v);

            // ASSERT
            string[] assertRows = new string[]
            {
                    "Sed ut perspiciatis unde omnis iste natus error sit voluptatem",
                    "accusantium doloremque laudantium, totam rem aperiam, eaque ipsa",
                    "quae ab illo inventore veritatis et quasi architecto beatae vitae",
                    "dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit",
                    "aspernatur aut odit aut fugit, sed quia consequuntur magni dolores",
                    "eos qui ratione voluptatem sequi nesciunt."
            };
            Assert.IsTrue(ret == String.Join(Environment.NewLine, assertRows));
        }
    }
}
