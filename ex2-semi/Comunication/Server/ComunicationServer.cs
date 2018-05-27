﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


using System.Configuration;
using System.IO;
using Comunication.Server;
using Logging.Modal;
using Comunication.Event;

namespace Comunication.Server
{
   public class ComunicationServer
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
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        ch.HandleClient(client);
                        EventHandler<MessageRecievedEventArgs> shnitazel = null;
                        shnitazel = delegate (object sender, MessageRecievedEventArgs e)
                        {
                            //send to client orders
                            try
                            {
                                NetworkStream stream = client.GetStream();
                                BinaryWriter writer = new BinaryWriter(stream);
                                {
                                    ServerDataReciecedEventArgs message = new ServerDataReciecedEventArgs("Log", (int)e.Status + ":" + e.Message);
                                    writer.Write(message.ToJSON());
                                }
                                //if send-error stop listen
                            } catch(Exception)
                            {
                                GUICommandRecieved -= shnitazel;
                                client.Close();
                            }
                        };
                        GUICommandRecieved += shnitazel;


                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
            });
            task.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }

        public void SendNewLog(object sender, MessageRecievedEventArgs e)
        {
            try
            {
                GUICommandRecieved(this, e);
            } catch(Exception)
            {
                return;
            }
        }
    }
}
