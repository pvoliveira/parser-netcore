using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD.FileReader;

namespace TDD.Tests
{
    [TestClass]
    public class ParserTests
    {
        private string tmpFilePath;

        [TestInitialize]
        public void Initializer()
        {
            this.tmpFilePath = Path.GetTempFileName();
        }

        [TestMethod]
        public void Read_ValidFile_EnumerableOfString()
        {
            // arrange
            var fileBytes = Encoding.UTF8.GetBytes("line1\nline2");
            File.WriteAllBytes(this.tmpFilePath, fileBytes);

            using(var parser = new Parser(this.tmpFilePath))
            {
            
                Func<byte[], string> func = (raw) => Encoding.UTF8.GetString(raw);

                // act
                var results = parser.Run(func);

                // assert
                Assert.IsNotNull(results);
                foreach (var chr in results)
                {
                    Console.WriteLine(chr);
                }
            }
        }

        [TestMethod]
        public void Run_InvalidFilePath_FileNotFoundException()
        {
            // arrange
            var emptyFile = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());
            
            // act
            Action act = () => new Parser(emptyFile);

            // assert
            Assert.ThrowsException<FileNotFoundException>(act);
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (!string.IsNullOrWhiteSpace(this.tmpFilePath) && File.Exists(this.tmpFilePath))
            {
                File.Delete(this.tmpFilePath);
            }
        }
    }
}
