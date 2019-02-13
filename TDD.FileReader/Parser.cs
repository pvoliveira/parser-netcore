using System;
using System.Collections.Generic;
using System.IO;

namespace TDD.FileReader
{
    public class Parser
    {
        private string path;

        public Parser(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File can not be found: {path}", path);
            }

            this.path = path;
        }

        public IEnumerable<object> Run(Func<byte[], string> run)
        {
            var file = File.Open(this.path, FileMode.Open);
            
            const int length = 1024;
            byte[] buffer = new byte[length];
            while (file.Read(buffer, 0, length) > 0)
            {
                yield return run(buffer);
            }
        }
    }
}