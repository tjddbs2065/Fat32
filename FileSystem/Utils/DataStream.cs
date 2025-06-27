using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Utils
{
    internal class DataStream
    {
        private FileStream _stream;
        private string _filePath;
        private int _offset;

        public DataStream(string path)
        {
            _filePath = path;
            _stream = File.OpenRead(_filePath);
        }

        ~DataStream() { _stream.Close(); }

        public byte[] GetBytes(int length)
        {
            byte[] bytes = new byte[length];
            _stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }
        public byte[] GetBytes(int offset, int length)
        {
            byte[] bytes = new byte[length];
            SetOffset(offset);
            _stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }
        public void SetOffset(int offset)
        {
            _stream.Seek(offset, SeekOrigin.Begin);
        }

    }
}
