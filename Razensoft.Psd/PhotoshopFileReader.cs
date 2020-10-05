using System;
using System.Drawing;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Razensoft.Psd.Debug;

namespace Razensoft.Psd
{
    internal class PhotoshopFileReader : IDisposable
    {
        [NotNull] private readonly BinaryReader _binaryReader;

        public PhotoshopFileReader([NotNull] byte[] buffer)
        {
            Assert.NotNull(buffer, nameof(buffer));
            var stream = new MemoryStream(buffer);
            _binaryReader = new BinaryReader(stream);
        }

        public PhotoshopFileReader([NotNull] Stream input)
        {
            Assert.NotNull(input, nameof(input));
            _binaryReader = new BinaryReader(input);
        }

        [NotNull]
        public byte[] ReadBytes(int count) => _binaryReader.ReadBytes(count);

        public double ReadByte() => _binaryReader.ReadByte();

        public double ReadDouble()
        {
            var data = ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToDouble(data, 0);
        }

        public short ReadInt16()
        {
            var data = ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToInt16(data, 0);
        }

        public int ReadInt32()
        {
            var data = ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToInt32(data, 0);
        }

        public long ReadInt64()
        {
            var data = ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToInt64(data, 0);
        }

        public float ReadSingle()
        {
            var data = ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToSingle(data, 0);
        }

        [NotNull]
        public string ReadString()
        {
            var length = ReadInt32();
            var value = ReadString(length);
            ReadBytes(2);
            return value;
        }

        [NotNull]
        public string ReadString(int length)
        {
            var data = ReadBytes(length * 2);
            SwitchEndianness(data, 2);
            return Encoding.Unicode.GetString(data);
        }

        public ushort ReadUInt16()
        {
            var data = ReadBytes(2);
            Array.Reverse(data);
            return BitConverter.ToUInt16(data, 0);
        }

        public uint ReadUInt32()
        {
            var data = ReadBytes(4);
            Array.Reverse(data);
            return BitConverter.ToUInt32(data, 0);
        }

        public ulong ReadUInt64()
        {
            var data = ReadBytes(8);
            Array.Reverse(data);
            return BitConverter.ToUInt64(data, 0);
        }

        public Rectangle ReadRectangle()
        {
            var top = ReadInt32();
            var left = ReadInt32();
            var bottom = ReadInt32();
            var right = ReadInt32();
            return Rectangle.FromLTRB(
                left,
                top,
                right,
                bottom
            );
        }

        private static void SwitchEndianness([NotNull] byte[] bytes, int stride)
        {
            Assert.NotNull(bytes, nameof(bytes));
            Assert.That((stride & (stride - 1)) == 0, "Stride must be a power of 2");
            Assert.That(bytes.Length % stride == 0, "Byte array length must be divisible by stride");

            var sections = bytes.Length / stride;
            for (var i = 0; i < sections - 1; i++)
            {
                Array.Reverse(bytes, i * stride, stride);
            }
        }

        public void Dispose() => _binaryReader.Dispose();
    }
}
