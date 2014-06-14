﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace GravityTutorial
{
    public class Client
    {
        Socket server_socket;
        
        StreamWriter servWriter;
        StreamReader servReader;

        public bool Connected { get { return server_socket.Connected; } }

        public Client()
        {
            server_socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string host, int port, string name)
        {
            try
            {
                server_socket.Connect(host, port);
                Ressource.connected = true;
            }
            catch (SocketException)
            {
                //Console.WriteLine("Connexion to server failed");
                return;
            }


            servWriter = new StreamWriter(new NetworkStream(server_socket));
            servReader = new StreamReader(new NetworkStream(server_socket));

            servWriter.WriteLine(name);
            servWriter.Flush();
            string confirm = servReader.ReadLine();
            if (confirm.CompareTo("Welcome") == 0)
                Console.WriteLine("Confirmation  : " + confirm);
            else
            {
                Close();
                server_socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("You're blacklisted, get lost");
            }
        }

        /*public void Run()
        {
            Thread reader = new Thread(new ThreadStart(Read));
            Thread writer = new Thread(new ThreadStart(Write));

            reader.Start();
            writer.Start();

            reader.Join();
            writer.Join();
            Close();
        }*/

        public void Read()
        {
            if (server_socket.Poll(1, SelectMode.SelectRead))
            {
                string s = (servReader.ReadLine());
                if (s == null || s == "")
                {
                    return;
                }
                Ressource.messageFromJ1 = s;

                
            }
        }

        public void Write()
        {
                servWriter.WriteLine(Level.KeyboardToStr());
                try
                {
                    servWriter.Flush();
                }
                catch (System.IO.IOException)
                {
                    Game1.reseauLost = true;
                    
                }
                
        }

        private void Close()
        {
            servWriter.Flush();
            servWriter.Close();
            servReader.Close();
            server_socket.Close();
        }
    }
}
