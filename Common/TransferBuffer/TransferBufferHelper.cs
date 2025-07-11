﻿using System;
using System.Collections;
using System.Text;

namespace Common.TransferBuffer
{
    /// <summary>
    /// 双方端一致可以直接解析
    ///如果双方的数据类型不一致，可能会导致数据解析错误专门用来解析
    ///isLittleEndian=其他系统是否为小端存储
    ///BitConverter.IsLittleEndian 本系统是否为小端
    /// </summary>
    public static class TransferBufferHelper
    {

        public static bool GetBool(byte buffer,byte index)
        {
            BitArray bitArray = new BitArray(new[] { buffer });
            return bitArray[index];
        }

        #region  PLC的Word类型对应ushort 此处为Plc Word类型操作
        /// <summary>
        /// Word类型转换
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="isLittleEndian">其他系统是否为小端存储</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static ushort WordFromByteArray(byte[] bytes,bool isLittleEndian=false)
        {
            if (bytes.Length != 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }

            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.ToUInt16(bytes, 0);
            }
            else
            {
                return (ushort)((bytes[0] << 8) | bytes[1]);
            }
        }

        /// <summary>
        /// / Word类型转换为byte数组 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isLittleEndian">对方是否为小端</param>
        /// <returns></returns>
        public static byte[] UshortToByteArray(ushort value, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                byte[] array = new byte[2];
                array[1] = (byte)(value & 0xFFu);
                array[0] = (byte)((uint)(value >> 8) & 0xFFu);
                return array;
            }
        }
        #endregion

        #region  PLC的Int类型对应short 此处为Plc Int类型操作
        public static short IntFromByteArray(byte[] bytes, bool isLittleEndian = false)
        {
            if (bytes.Length != 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.ToInt16(bytes);
            }
            else
            {
                return (short)(bytes[1] | (bytes[0] << 8));
            }
        }

       
        public static byte[] IntToByteArray(short value, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                return new byte[2]
                {
                    (byte)((uint)(value >> 8) & 0xFF),
                    (byte)((uint)value & 0xFF)
                };
            }
        }
        #endregion

        #region PLC的DWord类型对应uint 此处为Plc DWord类型操作

        public static uint DWordFromByteArray(byte[] bytes, bool isLittleEndian = false)
        {
            if(bytes.Length != 4)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 4 bytes.");
            }
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.ToUInt32(bytes, 0);
            }
            else
            {
                return (uint)((bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3]);
            }
        }

        public static byte[] DWordToByteArray(uint value, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                return new byte[4]
                {
                    (byte)((value >> 24) & 0xFFu),
                    (byte)((value >> 16) & 0xFFu),
                    (byte)((value >> 8) & 0xFFu),
                    (byte)(value & 0xFFu)
                };
            }
        }

        #endregion

        #region PLC的DInt类型对应int 此处为Plc DInt类型操作
        public static int DIntFromByteArray(byte[] bytes, bool isLittleEndian = false)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 4 bytes.");
            }
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.ToInt32(bytes, 0);
            }
            else
            {
                return (bytes[3] | (bytes[2] << 8) | (bytes[1] << 16) | (bytes[0] << 24));
            }
        }

        public static byte[] DIntToByteArray(int value, bool isLittleEndian = false)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                return new byte[4]
                {
                    (byte)((uint)(value >> 24) & 0xFFu),
                    (byte)((uint)(value >> 16) & 0xFFu),
                    (byte)((uint)(value >> 8) & 0xFFu),
                    (byte)((uint)value & 0xFFu)
                };
            }
        }
        #endregion

        #region Plc的Real 类型对应 Float 此处为Plc Real类型操作
        public static float RealFromByteArray(byte[] bytes,bool isLittleEndian)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 4 bytes.");
            }

            if (BitConverter.IsLittleEndian==isLittleEndian)
            {
                bytes = new byte[4]
                {
                bytes[3],
                bytes[2],
                bytes[1],
                bytes[0]
                };
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        public static byte[] RealToByteArray(float value, bool isLittleEndian = false)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian==isLittleEndian)
            {
                return bytes;
            }

            return new byte[4]
            {
            bytes[3],
            bytes[2],
            bytes[1],
            bytes[0]
            };
        }
        #endregion

        #region PLC的LReal类型对应Double 此处为Plc LReal类型操作
        public static double LRealFromByteArray(byte[] bytes,bool isLittleEndian)
        {
            if (bytes.Length != 8)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 8 bytes.");
            }

            if (BitConverter.IsLittleEndian==isLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        public static byte[] LRealToByteArray(double value, bool isLittleEndian)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }
        #endregion

        #region PLC的String类型对应string 此处为Plc string类型操作
        public static byte[] StringToByteArray(string value, int reservedLength)
        {
            byte[] array = new byte[reservedLength];
            if (value == null)
            {
                return array;
            }

            int num = value.Length;
            if (num == 0)
            {
                return array;
            }

            if (num > reservedLength)
            {
                num = reservedLength;
            }

            Encoding.ASCII.GetBytes(value, 0, num, array, 0);
            return array;
        }

        public static string StringFromByteArray(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
        #endregion

        #region PLC的S7String对应string 此处为Plc S7String类型操作
        public static string S7StringFromByteArray(byte[] bytes,Encoding stringEncoding)
        {
            if (bytes.Length < 2)
            {
                throw new ArgumentException( "Malformed S7 String / too short");
            }

            int num = bytes[0];
            int num2 = bytes[1];
            if (num2 > num)
            {
                throw new ArgumentException("Malformed S7 String / length larger than capacity");
            }

            try
            {
                return stringEncoding.GetString(bytes, 2, num2);
            }
            catch (Exception inner)
            {
                throw new ArgumentException( $"Failed to parse {9} from data. Following fields were read: size: '{num}', actual length: '{num2}', total number of bytes (including header): '{bytes.Length}'.", inner);
            }
        }

        public static byte[] ToByteArray(string? value, int reservedLength,Encoding stringEncoding)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (reservedLength > 254)
            {
                throw new ArgumentException("The maximum string length supported is 254.");
            }

            byte[] bytes = stringEncoding.GetBytes(value);
            if (bytes.Length > reservedLength)
            {
                throw new ArgumentException($"The provided string length ({bytes.Length} is larger than the specified reserved length ({reservedLength}).");
            }

            byte[] array = new byte[2 + reservedLength];
            Array.Copy(bytes, 0, array, 2, bytes.Length);
            array[0] = (byte)reservedLength;
            array[1] = (byte)bytes.Length;
            return array;
        }
        #endregion
    }
}
