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
        public Boolean ConnectedToServer { get { return this.listenToServer; } }

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
            if (listenToServer != true)
            {
                //basicClient = new BasicClient(); ori moved
                basicClient.Ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                basicClient.Client = new TcpClient();
                try
                {
                    listenToServer = basicClient.ConnectToServer();
                    StartListenToServer();

                }
                catch (Exception e)
                {
                    // couldn't connet to server
                    listenToServer = false;
                    ServerMassages(this, new ServerDataReciecedEventArgs("Log", "2:No connection to server, what a shame =("));
                    ServerMassages(this, new ServerDataReciecedEventArgs("Log", "2:" + e.Message));
                }
            }

        }

        public void StartListenToServer()
        {
            Task task = new Task(() =>
            {
                while(this.listenToServer)
                {
                    try
                    { 
                    string data = basicClient.ReadDataFromServer();
                    ServerMassages(this, ServerDataReciecedEventArgs.FromJSON(data));
                    }
                    catch (Exception)
                    {
                        ServerMassages(this, new ServerDataReciecedEventArgs("Log", "2:Disconnected from server-bye"));
                        this.listenToServer = false;
                        Disconnect();
                        break;
                    }
                }

            });
            task.Start();
        }

        public void Disconnect()
        {
            basicClient.CloseClient();
        }

        public void SendMessage(string message)
        {
            Task task = new Task(() =>
            {
                try
                {
                    //Sets the stream write into it
                    basicClient.SendDataToServer(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });
            task.Start();
        }

    }
}
