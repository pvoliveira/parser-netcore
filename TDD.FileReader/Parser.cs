using System;
using System.Collections.Generic;
using System.IO;

namespace TDD.FileReader
{
    public class Parser: IDisposable
    {
        private int bufferLength;
        private FileStream file;
        private string path;


        public Parser(string path, int bufferLength = 1024) // buffer length 1KB
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File can not be found: {path}", path);
            }

            this.bufferLength = bufferLength;
            this.path = path;
        }

        public void Dispose()
        {
            if (file != null)
            {
                file.Close();
            }
        }

        public IEnumerable<object> Run(Func<byte[], string> run)
        {
            file = File.Open(this.path, FileMode.Open);
            
            byte[] buffer = new byte[this.bufferLength];
            while (file.Read(buffer, 0, this.bufferLength) > 0)
            {
                yield return run(buffer);
            }
        }
    }
}