using ImageService.Controller;
using Logging.Modal;
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
                try
                {


                    NetworkStream stream = client.GetStream();
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    string commandLine = "";
                    while (!commandLine.Equals("Close Client"))
                    {
                        //read first message from client.
                        commandLine = reader.ReadString();
                        if (commandLine.Equals("Close Client")) break;
                        bool check;
                        string[] command = commandLine.Split(':');
                        string result = m_controller.ExecuteCommand(Int32.Parse(command[0]), command, out check);
                        //send to client the result of the command
                        writer.Write(result);
                    }
                    client.Close();
                } catch(Exception)
                {
                    client.Close();
                }

            }).Start();
        }
    }
}
    
