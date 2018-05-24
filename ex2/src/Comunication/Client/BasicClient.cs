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
        private static BasicClient instance;
        private BasicClient() { }
        public static BasicClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BasicClient();
                }
                return instance;
            }
        }
       public IPEndPoint Ep { get; set; }
       public TcpClient Client { get; set; }
       private Boolean run { get; set; }

        /// <summary>
        /// connect client to server
        /// </summary>
        public void ConnectToServer()
        {
            this.Client.Connect(Ep);
            this.run = true;
        }
        /// <summary>
        /// send string message to server
        /// </summary>
        /// <param name="message"></param>
        public void SendDataToServer(string message)
        {
            using (NetworkStream stream = this.Client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                writer.Write(message);
            }
        }
        /// <summary>
        /// read message-string from server
        /// </summary>
        /// <returns></returns>
        public string ReadDataFromServer()
        {
            string data;
            using (NetworkStream stream = this.Client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                        // Get data from server
                        data = reader.ReadString();
            }
            return data;
        }
        /// <summary>
        /// close the connection to server;
        /// </summary>
         public void CloseClient()
        {
            this.Client.Close();
            this.run = false;
        }
    }
}
