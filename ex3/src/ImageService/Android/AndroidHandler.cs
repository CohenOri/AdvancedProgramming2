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

                string data = "";
                byte[] name;
                int type = 0;
                byte[] image = null;
                type = reader.Read();
                image = reader.ReadBytes(type);
                while (type != 1)
                {
                    type = reader.Read();
                    //read the type message
                    if (type == 0 || type == 1) break;
                    //reade image byte array
                    image = reader.ReadBytes(type);
                    //reade name
                    type = reader.Read();
                    //read the type message
                     if (type == 0 || type == 1) break;
                    //reade image byte array
                    name = reader.ReadBytes(type);
                     MovePicture(image, name);
                    }
                    //if client closed
                    client.Close();
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
