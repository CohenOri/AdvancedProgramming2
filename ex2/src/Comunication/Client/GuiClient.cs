using Comunication.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Comunication.Client
{
    class GuiClient
    {
        private Boolean listenToServer;
        public event EventHandler<ServerDataReciecedEventArgs> ServerMassages;
        public GuiClient()
        {
            BasicClient.Instance.Ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            BasicClient.Instance.Client =  new TcpClient();
            BasicClient.Instance.ConnectToServer();
            listenToServer = true;
        }

        public void StartListenToServer()
        {
            Task task = new Task(() =>
            {
                while(this.listenToServer)
                {
                    string data = BasicClient.Instance.ReadDataFromServer();
                    ServerMassages(this,ServerDataReciecedEventArgs.FromJSON(data));
                }

            });
        }

        public void Disconnect()
        {
            BasicClient.Instance.CloseClient();
        }

        public void SendMessage(string message)
        {
            BasicClient.Instance.SendDataToServer(message);
        }

    }
}
