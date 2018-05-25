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
   public class GuiClient
    {
        private Boolean listenToServer;
        private BasicClient basicClient;
        public event EventHandler<ServerDataReciecedEventArgs> ServerMassages;

        private static GuiClient instance = null;
        public static GuiClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GuiClient();
                }
                return instance;
            }
        }

        private GuiClient()
        {
            basicClient = new BasicClient(); // ori added
        }

        public void Connect()
        {

            //basicClient = new BasicClient(); ori moved
            basicClient.Ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            basicClient.Client =  new TcpClient();
            basicClient.ConnectToServer();
            listenToServer = true;
        }

        public void StartListenToServer()
        {
            Task task = new Task(() =>
            {
                while(this.listenToServer)
                {
                    string data = basicClient.ReadDataFromServer();
                    ServerMassages(this,ServerDataReciecedEventArgs.FromJSON(data));
                }

            });
        }

        public void Disconnect()
        {
            basicClient.CloseClient();
        }

        public void SendMessage(string message)
        {
            basicClient.SendDataToServer(message);
        }

    }
}
