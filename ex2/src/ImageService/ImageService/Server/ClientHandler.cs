using ImageService.Controller;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    class ClientHandler : IClientHandler
    {
        private IImageController m_controller;
        public ClientHandler(IImageController controller)
        {
            this.m_controller = controller;
        }
        public void HandleClient(TcpClient client)
        {

            new Task(() =>
            {
            using (NetworkStream stream = client.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                //read first message from client.
                string commandLine = reader.ReadLine();
                Console.WriteLine("Got command: {0}", commandLine);
                bool check;
                string result = m_controller.ExecuteCommand(Int32.Parse(commandLine), null, out check);
                //send to client all the logs.
                writer.Write(result);

                }
                //client.Close();

            }).Start();
        }
        /// <summary>
        /// send to client the 
        /// s that added
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private string SendCurrentLogs(string commandLine, TcpClient ch)
        {
            return "c\n";
        }

        private string ListentToRemovingItems(TcpClient ch)
        {
            using (NetworkStream stream = ch.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string commandLine = reader.ReadLine();
                return commandLine;
            }
            
        }

        private void SendNewLog(string massage, TcpClient ch)
        {
            using (NetworkStream stream = ch.GetStream())
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(massage);
            }
        }
        


    }
}
    
