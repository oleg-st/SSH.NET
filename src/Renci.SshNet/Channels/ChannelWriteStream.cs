using System;
using System.IO;

namespace Renci.SshNet.Channels
{
    internal class ChannelWriteStream : Stream
    {
        private readonly IChannel _channel;
        private bool _isDisposed;

        public ChannelWriteStream(IChannel channel)
        {
            _channel = channel;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_isDisposed)
            {
                throw new EndOfStreamException();
            }

            _channel.SendData(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (!_isDisposed && _channel.IsOpen)
                {
                    _channel.SendEof();
                }

                _isDisposed = true;
            }
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
    }
}
