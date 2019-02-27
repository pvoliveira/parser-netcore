using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TDD.FileReader
{
    public class Parser
    {
        private int bufferLength;
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

        public IEnumerable<object> Run(Func<byte[], string> run)
        {
            using(var file = File.Open(this.path, FileMode.Open))
            {            
                byte[] buffer = new byte[this.bufferLength];
                while (file.Read(buffer, 0, this.bufferLength) > 0)
                {
                    yield return run(buffer);
                }
            }
        }

        public ChannelReader<string> RunAsync(Func<byte[], string> run, CancellationToken cancellationToken = default(CancellationToken))
        {
            var channel = Channel.CreateBounded<string>(1);
            
            Task.Run(async () => {
                using(var file = File.Open(this.path, FileMode.Open))
                {            
                    byte[] buffer = new byte[this.bufferLength];
                    while (file.Read(buffer, 0, this.bufferLength) > 0)
                    {
                        var v = run(buffer);
                        while(await channel.Writer.WaitToWriteAsync(cancellationToken))
                            if (channel.Writer.TryWrite(v)) break;
                    }
                }
            }, cancellationToken);

            return channel;
        }
    }
}