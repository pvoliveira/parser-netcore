using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDD.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Read_ValidFile_EnumerableOfString()
        {
            // arrange
            parser = new Parser("./test.txt");
            
            // act
            var results = parser.Run(_ => Console.WriteLine(_));

             // assert
             Assert.results
        }
    }
}
