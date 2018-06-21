using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                    int picLengthInBytes = 0;
                    int picNameLengthInBytes = 0;
                    byte[] imageByteArr = null;
                    byte[] name;
                    picLengthInBytes = reader.ReadInt32(); // read length
                    picNameLengthInBytes = reader.ReadInt32(); // read pic Name length

                    int numBytesRead = 0;
                    while (picLengthInBytes > 0) // read the client's image byte arr to imageByteArr
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = reader.Read(imageByteArr, numBytesRead, picLengthInBytes);

                        // Break when the end of the file is reached.
                        if (n == 0) { break; }
                        numBytesRead += n;
                        picLengthInBytes -= n;
                    }

                    name = reader.ReadBytes(picNameLengthInBytes); // read pic Name
                    MovePicture(imageByteArr, name);
                    //if client closed       client.Close();
                }
                catch (Exception)
                {
                    client.Close();
                }

            }).Start();
        }
        private void MovePicture(byte[] info, byte[] fileName)
        {
            Image a = ByteArrayToImage(info);
            a.Save(this.DirectoryPath + "\\" + GetString(fileName));

        }

        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

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
        }
    }
}
