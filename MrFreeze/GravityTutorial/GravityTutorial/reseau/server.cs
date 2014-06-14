using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GravityTutorial
{
    public class Server
    {
        public LinkedList<Client_server> clients;
        public Socket socket;

        public string message;
        
        public Server(int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            clients = new LinkedList<Client_server>();
            socket.Listen(35);
            Console.WriteLine("Connected on port " + ((IPEndPoint)socket.LocalEndPoint).Port);
        }

        public void Run()
        {
            Thread clientsThread = new Thread(new ThreadStart(AcceptClient));
            clientsThread.Start();

            //Thread clientsChat = new Thread(new ThreadStart(Chat));
            //clientsChat.Start();

            clientsThread.Join();
            //clientsChat.Join();
        }

        public void AcceptClient()
        {
            //while (true)
            //{
                Socket client = socket.Accept();
                // Use the socket client to do whatever you want to do
                IPEndPoint remote = (IPEndPoint)client.RemoteEndPoint;
                Client_server myClient = new Client_server("player2", remote.Address.ToString(), remote.Port, client);
                myClient.Send("Welcome");

                clients.AddLast(myClient);
                Ressource.serverCount += 1;
                Console.WriteLine("Connexion from " + myClient.name);
            //}
        }

        public static void getIp()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            Ressource.ipJ1 = localIP;
        }

        public void Chat()
        {
            //while (true)
           // {
                if ((clients.Count > 0))
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        Client_server client = clients.ElementAt(i);

                        if (client.sock.Poll(1000, SelectMode.SelectRead))
                        {
                            message = client.Receive();
                            Ressource.keybordFromJ2ToJ1 = message;
                            if (message == null)
                            {
                                Console.WriteLine("Client " + client.name + " disconnected");
                                clients.Remove(client);
                                //continue;
                            }

                            //Console.WriteLine(client.name + " : " + message);

                            foreach (Client_server sclient in clients)
                            {
                                if (Game1.level == null)
                                {
                                    sclient.Send(Ressource.messageJ1toJ2);
                                }
                                else
                                {
                                    sclient.Send(Ressource.messageJ1toJ2 + Game1.level.LvlToStr(Hud.youlose, Hud.youwin));
                                }
                                
                            }
                                
                        }
                   // }
              }
            }
        }
        public string connected()
        {
            if (socket.Connected)
            {
                return "connection ...";
            }
            else
                return "fail connection";
        }
    }
}
