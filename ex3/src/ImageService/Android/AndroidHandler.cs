using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading;

namespace ImageService.Android
{
    class AndroidHandler
    {
        private string DirectoryPath;
        public AndroidHandler(string path)
        {
            DirectoryPath = path;
        }
        public void HandleClient(TcpClient client)
        {
            //create a task witch will listen to  client command and execute them.
            new Task(() =>
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    // read image name
                    List<Byte> byteList = new List<Byte>();
                    Byte[] b = new Byte[1];
                    // read first byte
                    stream.Read(b, 0, 1);
                    byteList.Add(b[0]);
                    while (stream.DataAvailable) // read rest
                    {
                        stream.Read(b, 0, 1);
                        byteList.Add(b[0]);
                    }
                    String imgName = Path.GetFileNameWithoutExtension(System.Text.Encoding.UTF8.GetString(byteList.ToArray()));

                    Byte[] imgByte = new Byte[1]; 
                    imgByte[0] = 1;
                
                    stream.Write(imgByte, 0, 1); // notify client finished reading the name

                    Byte[] len = new Byte[8];
                    stream.Read(len, 0, 8);
                    long length = BitConverter.ToInt64(len, 0);
                    stream.Write(imgByte, 0, 1); // notify client finished reading the length


                    //byte[] imgByteArr = ReadImage(stream);
                    byte[] imgByteArr = ReadImage(stream, length);


                    stream.Write(imgByte, 0, 1); // notify client finished reading the img

                    File.WriteAllBytes(this.DirectoryPath + "\\" + imgName + ".PNG", imgByteArr);



                    /*                   long picLengthInBytes = 0;
                                   long picNameLengthInBytes = 0;
                                   byte[] imageByteArr = null;
                                   byte[] name;
                                   picLengthInBytes = reader.ReadInt64(); // read length

                               if(picLengthInBytes == -1) // "-1" signals client closed...
                                   {
                                       client.Close();
                                   } else
                                   {
                                       picNameLengthInBytes = reader.ReadInt64(); // read pic Name length

                                       int numBytesRead = 0;
                                       while (picLengthInBytes > 0) // read the client's image byte arr to imageByteArr
                                       {
                                           // Read may return anything from 0 to numBytesToRead.
                                           int n = reader.Read(imageByteArr, numBytesRead, (int)picLengthInBytes);

                                           // Break when the end of the file is reached.
                                           if (n == 0) { break; }
                                           numBytesRead += n;
                                           picLengthInBytes -= n;
                                       }

                                       name = reader.ReadBytes((int)picNameLengthInBytes); // read pic Name
                                       MovePicture(imageByteArr, name);
                                   }
                                   */
                }
                catch (Exception)
                {
                    client.Close();
                }

            }).Start();
        }

        /*private byte[] ReadImage(NetworkStream stream)
        {
            List<Byte> byteList = new List<Byte>();
            int i = 0;
            Byte[] b = new Byte[1];
            // read first byte (wait until there's such...)
            i = stream.Read(b, 0, b.Length);
            byteList.Add(b[0]);
            while (stream.DataAvailable) // read rest
            {
                i = stream.Read(b, 0, b.Length);
                byteList.Add(b[0]);
            }
            return byteList.ToArray();
        }*/

        private byte[] ReadImage(NetworkStream stream, long len)
        {
            int readBytes = 0;
            byte[] bytes = new byte[len];
            while(readBytes < len)
            {
                readBytes += stream.Read(bytes, readBytes, ((int)len - readBytes));
            }
            return bytes;
        }

        /*private void MovePicture(byte[] info, byte[] fileName)
        {
            Image a = ByteArrayToImage(info);
            a.Save(this.DirectoryPath + "\\" + GetString(fileName));

        }*/

        /*private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }*/

        /*
    /// <summary>
    /// Create picture object from ByteArray
    /// </summary>
    /// <param name="bytesArr"></param>
    /// <returns></returns>
    private Image ByteArrayToImage(byte[] bytesArr)
    {
        MemoryStream memstr = new MemoryStream(bytesArr);
        Image img = Image.FromStream(memstr);
        return img;
    }*/
    }
}
