using ImageService.Controller;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Comunication.Server
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
                string commandLine = "";
                    while (!commandLine.Equals("stop"))
                    {
                        //read first message from client.
                        commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        bool check;
                        string[] command = commandLine.Split(':');
                        string result = m_controller.ExecuteCommand(Int32.Parse(command[0]), command, out check);
                        //send to client all the logs.
                        writer.Write(result);
                    }
                   client.Close();
                }

            }).Start();
        }
    }
}
    
