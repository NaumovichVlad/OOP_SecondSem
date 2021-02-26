using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace StreamEditorLibrary
{
    public class StreamDecorator : Stream
    {
        protected Stream stream;
        Stopwatch totalTimer = new Stopwatch();
        Stopwatch useFulTimer = new Stopwatch();

        public StreamDecorator(Stream stream) : base()
        {
            totalTimer.Start();
            this.stream = stream;
        }

        public double TotalTime => totalTimer.Elapsed.TotalMilliseconds;

        public double UsefulTime => useFulTimer.Elapsed.TotalMilliseconds;

        public override bool CanRead => stream.CanRead;

        public override bool CanSeek => stream.CanSeek;

        public override bool CanWrite => stream.CanWrite;

        public override long Length => stream.Length;

        public override long Position { get => stream.Position; set => stream.Position = value; }

        public override void Flush()
        {
            useFulTimer.Start();
            stream.Flush();
            useFulTimer.Stop();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            useFulTimer.Start();
            int returnValue = stream.Read(buffer, offset, count);
            useFulTimer.Stop();

            return returnValue;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            useFulTimer.Start();
            long returnValue = stream.Seek(offset, origin);
            useFulTimer.Stop();
            return returnValue;
        }

        public override void SetLength(long value)
        {
            useFulTimer.Start();
            stream.SetLength(value);
            useFulTimer.Stop();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            useFulTimer.Start();
            stream.Write(buffer, offset, count);
            useFulTimer.Stop();
        }

        public double ComputeEfficiency()
        {
            double efficiency;
            totalTimer.Stop();
            efficiency = Math.Round(UsefulTime / TotalTime * 100, 3);
            totalTimer.Start();
            return efficiency;
        }
    }
}
