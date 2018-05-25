using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Comunication.Client
{
    class BasicClient
    {
        public BasicClient()
        {
            //this.Client = new TcpClient(); // ori added
        }

        public IPEndPoint Ep { get; set; }
       public TcpClient Client { get; set; }
       private Boolean run { get; set; }

        /// <summary>
        /// connect client to basicClient
        /// </summary>
        public void ConnectToServer()
        {
            this.Client.Connect(Ep);
            this.run = true;
        }
        /// <summary>
        /// send string message to basicClient
        /// </summary>
        /// <param name="message"></param>
        public void SendDataToServer(string message)
        {
            using (NetworkStream stream = this.Client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to basicClient
                writer.Write(message);
            }
        }
        /// <summary>
        /// read message-string from basicClient
        /// </summary>
        /// <returns></returns>
        public string ReadDataFromServer()
        {
            string data;
            using (NetworkStream stream = this.Client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                        // Get data from basicClient
                        data = reader.ReadString();
            }
            return data;
        }
        /// <summary>
        /// close the connection to basicClient;
        /// </summary>
         public void CloseClient()
        {
            this.Client.Close();
            this.run = false;
        }
    }
}
