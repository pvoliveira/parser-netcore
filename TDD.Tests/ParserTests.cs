using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD.FileReader;

namespace TDD.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Read_ValidFile_EnumerableOfString()
        {
            // arrange
            var parser = new Parser("./test.txt");
            
            Func<byte[], string> func = (raw) => Encoding.UTF8.GetString(raw);

            // act
            var results = parser.Run(func);

             // assert
             Assert.IsNotNull(results);
             Assert.IsInstanceOfType(results, typeof(IEnumerable<string>));
        }
    }
}
