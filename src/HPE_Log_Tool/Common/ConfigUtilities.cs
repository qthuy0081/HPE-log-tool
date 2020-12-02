using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Xml.Serialization;

namespace HPE_Log_Tool.Common
{
    public static class Utility
    {

        public static byte[] GetBytes(string stringBytes, char delimeter)
        {
            string[] arrayString = stringBytes.Split(delimeter);
            byte[] bytesResult = new byte[arrayString.Length];
            int counter = 0;
            foreach (string str in arrayString)
            {
                if (byte.TryParse(str, System.Globalization.NumberStyles.HexNumber, null, out byte tmpByte))
                {
                    bytesResult[counter] = tmpByte;
                    counter++;
                }
                else
                {
                    return null;
                }
            }
            return bytesResult;
        }

        public static byte[] GetBytes(string stringBytes)
        {
            string fString = "";
            int counter = 0;
            if (stringBytes.Trim() == "")
            {
                return null;
            }
            for (int i = 0; i < stringBytes.Length; i++)
            {
                if (stringBytes[i] == ' ')
                {
                    continue;
                }
                if (counter > 0)
                {
                    if ((counter % 2) == 0)
                    {
                        fString += " ";
                    }
                }
                fString += stringBytes[i].ToString();
                counter++;
            }
            return GetBytes(fString, ' ');
        }

        public static int ByteToInt(byte[] data, bool isLittleEndian)
        {
            byte[] tmpArry = new byte[data.Length];
            Array.Copy(data, tmpArry, tmpArry.Length);
            if (tmpArry.Length != 4)
            {
                if (isLittleEndian)
                {
                    Array.Resize(ref tmpArry, 4);
                }
                else
                {
                    Array.Reverse(tmpArry);
                    Array.Resize(ref tmpArry, 4);
                    Array.Reverse(tmpArry);
                }
            }
            if (isLittleEndian)
            {
                return (tmpArry[3] << 24) + (tmpArry[2] << 16) + (tmpArry[1] << 8) + tmpArry[0];
            }
            else
            {
                return (tmpArry[0] << 24) + (tmpArry[1] << 16) + (tmpArry[2] << 8) + tmpArry[3];
            }
        }

        public static int ByteToInt(byte[] data, int index, bool isLittleEndian)
        {
            byte[] tmpArry = new byte[4];
            Buffer.BlockCopy(data, index, tmpArry, 0, 4);
            if (tmpArry.Length != 4)
            {
                if (isLittleEndian)
                {
                    Array.Resize(ref tmpArry, 4);
                }
                else
                {
                    Array.Reverse(tmpArry);
                    Array.Resize(ref tmpArry, 4);
                    Array.Reverse(tmpArry);
                }
            }
            if (isLittleEndian)
            {
                return (tmpArry[3] << 24) + (tmpArry[2] << 16) + (tmpArry[1] << 8) + tmpArry[0];
            }
            else
            {
                return (tmpArry[0] << 24) + (tmpArry[1] << 16) + (tmpArry[2] << 8) + tmpArry[3];
            }
        }

        public static int ByteToInt(byte[] data)
        {
            return ByteToInt(data, false);
        }

        public static byte[] IntToByte(int nummber, bool isLittle)
        {
            byte[] tmpByte = new byte[4];
            tmpByte[0] = (byte)((nummber >> 24) & 0xFF);
            tmpByte[1] = (byte)((nummber >> 16) & 0xFF);
            tmpByte[2] = (byte)((nummber >> 8) & 0xFF);
            tmpByte[3] = (byte)(nummber & 0xFF);
            if (isLittle)
            {
                Array.Reverse(tmpByte);
            }
            return tmpByte;
        }

        public static byte[] IntToByte(int nummber)
        {
            byte[] tmpByte = new byte[4];
            tmpByte[0] = (byte)((nummber >> 24) & 0xFF);
            tmpByte[1] = (byte)((nummber >> 16) & 0xFF);
            tmpByte[2] = (byte)((nummber >> 8) & 0xFF);
            tmpByte[3] = (byte)(nummber & 0xFF);
            return tmpByte;
        }

        public static byte[] IntToByte(uint number)
        {
            byte[] tmpByte = new byte[4];
            tmpByte[0] = (byte)((number >> 24) & 0xFF);
            tmpByte[1] = (byte)((number >> 16) & 0xFF);
            tmpByte[2] = (byte)((number >> 8) & 0xFF);
            tmpByte[3] = (byte)(number & 0xFF);
            return tmpByte;
        }

        public static string ByteAsString(byte[] bytes, int startIndex, int length, bool spaceInBetween)
        {
            byte[] newByte;
            if (bytes.Length < startIndex + length)
            {
                Array.Resize(ref bytes, startIndex + length);
            }
            newByte = new byte[length];
            Array.Copy(bytes, startIndex, newByte, 0, length);
            return ByteAsString(newByte, spaceInBetween);
        }

        public static string ByteAsString(byte[] tmpbytes, bool spaceInBetween)
        {
            string tmpStr = string.Empty;
            if (tmpbytes == null)
            {
                return "";
            }
            for (int i = 0; i < tmpbytes.Length; i++)
            {
                tmpStr += string.Format("{0:X2}", tmpbytes[i]);
                if (spaceInBetween)
                {
                    tmpStr += " ";
                }
            }
            return tmpStr;
        }

        public static bool ByteArrayIsEqual(byte[] array1, byte[] array2, int lenght)
        {
            if (array1.Length < lenght)
            {
                return false;
            }
            if (array2.Length < lenght)
            {
                return false;
            }
            for (int i = 0; i < lenght; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ByteArrayIsEqual(byte[] array1, byte[] array2)
        {
            return ByteArrayIsEqual(array1, array2, array2.Length);
        }

        public static byte[] AppendArrays(byte[] array1, byte[] array2)
        {
            byte[] c = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, c, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, c, array1.Length, array2.Length);
            return c;
        }

        public static byte[] AppendArrays(byte[] array1, byte array2)
        {
            byte[] c = new byte[1 + array1.Length];
            Buffer.BlockCopy(array1, 0, c, 0, array1.Length);
            c[array1.Length] = array2;
            return c;
        }

        public static void IncrementAtIndex(byte[] array, int index)
        {
            if (array[index] == byte.MaxValue)
            {
                array[index] = 0;
                if (index > 0)
                {
                    IncrementAtIndex(array, index - 1);
                }
            }
            else
            {
                array[index]++;
            }
        }
        public static void checkExist(string path)
        {
            
            if (Directory.Exists(path))
            {
                MessageBox.Show("Folder exist");
            }
            else
            {
                MessageBox.Show("Folder does not exist");
            }
        }
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        #region Encrypt Method

        /// <summary>
        /// Encrypt/Decrypt AES 128 CBC mode
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="isEncrypt"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] input, byte[] key, byte[] iv, bool isEncrypt)
        {
            int inputLen = input.Length;
            if (inputLen % 16 != 0)
            {
                inputLen = (inputLen / 16 + 1) * 16;
            }
            if (inputLen == 16)
            {
                inputLen = 32;
            }
            byte[] output = new byte[inputLen];
            using (Aes myaes = Aes.Create())
            {
                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                myaes.Mode = CipherMode.CBC;
                //myaes.Padding = PaddingMode.Zeros;
                myaes.Padding = PaddingMode.PKCS7;
                myaes.BlockSize = 128;
                myaes.KeySize = 128;
                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor;
                if (isEncrypt)
                {
                    decryptor = myaes.CreateEncryptor(key, iv);
                }
                else
                {
                    decryptor = myaes.CreateDecryptor(key, iv);
                }
                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream(input);
                // Define cryptographic stream (always use Read mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                // Start decrypting.
                //int decryptedByteCount = cryptoStream.Read(output, 0, output.Length);
                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();
            }

            return output;
        }

        /// <summary>
        /// Encrypt/Decrypt AES 128 CTR mode
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputLength"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public static byte[] AESCTR128Encrypt(byte[] input, int inputLength, byte[] key, byte[] iv, byte[] counter)
        {
            byte[] output = new byte[inputLength];
            Queue<byte> xorCounter = new Queue<byte>();
            int numberBlock;
            int blockByteSize;
            byte[] internalCounter = new byte[16];
            Buffer.BlockCopy(counter, 0, internalCounter, 0, 16);
            // encrypt counter
            using (Aes myaes = Aes.Create())
            {
                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (EBC). Use default options for other symmetric key parameters.
                myaes.Mode = CipherMode.ECB;
                myaes.Padding = PaddingMode.None;
                myaes.BlockSize = 128;
                myaes.KeySize = 128;
                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor = myaes.CreateEncryptor(key, iv);
                MemoryStream memoryStream;
                CryptoStream cryptoStream;
                int decryptedByteCount;
                byte[] encryptCounter = new byte[16];
                blockByteSize = myaes.BlockSize / 8;
                if (inputLength % blockByteSize == 0)
                {
                    numberBlock = inputLength / blockByteSize;
                }
                else
                {
                    numberBlock = inputLength / blockByteSize + 1;
                }
                for (int i = 0; i < numberBlock; i++)
                {
                    // Define memory stream which will be used to hold encrypted data.
                    memoryStream = new MemoryStream(internalCounter);
                    // Define cryptographic stream (always use Read mode for encryption).
                    cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                    // Start decrypting.
                    decryptedByteCount = cryptoStream.Read(encryptCounter, 0, encryptCounter.Length);
                    // Close both streams.
                    memoryStream.Close();
                    cryptoStream.Close();
                    // Input to Queue
                    foreach (byte b in encryptCounter)
                    {
                        xorCounter.Enqueue(b);
                    }
                    // Increment counter
                    for (int j = counter.Length - 1; j >= 0; j--)
                    {
                        if (++internalCounter[j] != 0)
                        {
                            break;
                        }
                    }
                }
            }
            // XOR with input
            for (int k = 0; k < inputLength; k++)
            {
                byte mask = xorCounter.Dequeue();
                output[k] = (byte)(input[k] ^ mask);
            }
            return output;
        }

        public static byte[] Encrypt(byte[] data, byte[] password, byte[] iv)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;
                    AES.BlockSize = 128;
                    AES.KeySize = 256;
                    AES.Key = password;
                    AES.IV = iv;
                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }

        public static byte[] Decrypt(byte[] data, byte[] password, byte[] iv)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;
                    AES.BlockSize = 128;
                    AES.KeySize = 256;
                    AES.Key = password;
                    AES.IV = iv;
                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }
        #endregion

        #region Xml //quan trọng là mấy hàm SerializeXML vs deserialize để đọc vs save xml từ 1 model
        public static string SerializeXml<T>(T obj)
        {
            string xmlString;
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }

        public static T DeserialiseXmlLocal<T>(string xmlString)
        {
            if (xmlString.Length > 0)
            {
                xmlString = xmlString.Replace("NULL", "");
            }
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
            }
            return obj;
        }

        public static T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
        #endregion
    }
}
