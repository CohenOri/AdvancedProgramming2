using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Server;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System.Configuration;
using ImageService.Logging.Modal;
using System.IO;

namespace Comunication.Server
{
    class ComunicationServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        #region Properties
        public event EventHandler<MessageRecievedEventArgs> GUICommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion
        public ComunicationServer(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                        EventHandler<MessageRecievedEventArgs> shnitazel = null;
                        shnitazel = delegate (object sender, MessageRecievedEventArgs e)
                        {
                            //send to client orders
                            using (NetworkStream stream = client.GetStream())
                            using (StreamReader reader = new StreamReader(stream))
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                writer.Write("log:" + e.Status + ":" +  e.Message);
                            }
                            //if send-error stop listen
                            
                            GUICommandRecieved -= shnitazel;
                        };
                        GUICommandRecieved += shnitazel;


                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }

        public void SendNewLog(object sender, MessageRecievedEventArgs e)
        {
            GUICommandRecieved(this, e);
        }
    }
}
