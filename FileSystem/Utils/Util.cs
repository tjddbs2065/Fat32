using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.Utils
{
    internal class Util
    {
        public const int SECTOR = 512;
        public static uint ByteToUInt(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) 
                throw new ArgumentNullException("배열이 null이거나 비어있습니다.");

            if (bytes.Length == 1)
            {
                return bytes[0];
            }
            else if (bytes.Length == 2) return BitConverter.ToUInt16(bytes, 0);
            else if(bytes.Length >= 4) return BitConverter.ToUInt32(bytes, 0);
            else // 3바이트인 경우 4바이트로 확장 후 처리
            {
                byte[] padd = new byte[4];
                Array.Copy(bytes, padd, bytes.Length);
                return BitConverter.ToUInt32(padd, 0);
            }
        }

        public static string ByteToASCII(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
        public static string ByteToASCII(byte[] bytes, int length)
        {
            return Encoding.ASCII.GetString(bytes, 0, length).TrimEnd('\0').TrimEnd((char)0x20);
        }
        public static string ByteToASCII(byte[] bytes, int index, int length)
        {
            return Encoding.ASCII.GetString(bytes, index, length).TrimEnd('\0').TrimEnd((char)0x20);
        }
        public static string ByteToUnicode(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes).TrimEnd('\0').TrimEnd((char)0xFFFF);
        }
        public static string ByteToUnicode(byte[] bytes, int length)
        {
            return Encoding.Unicode.GetString(bytes, 0, length).TrimEnd('\0').TrimEnd((char)0xFFFF);
        }
        public static string ByteToUnicode(byte[] bytes, int index, int length)
        {
            return Encoding.Unicode.GetString(bytes, index, length).TrimEnd('\0').TrimEnd((char)0xFFFF);
        }

        public static byte[] CropBytes(byte[] bytes, int source, int size)
        {
            byte[] tmp = new byte[size];
            Array.Copy(bytes, source, tmp, 0, size);
            return tmp;
        }
        public static void PrintBytes(byte[] bytes)
        {
            int i = 0;
            foreach (byte b in bytes)
            {
                Console.Write($"{b:X2} ");
                if(++i == 16)
                {
                    Console.WriteLine();
                    i = 0;
                }
            }
        }
        public static void WriteFile(string filePath, byte[] data)
        {

        }
    }
}
