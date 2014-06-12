﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace WindowsGame1
{
    class Client_server
    {
        public String name { get; private set; }
        String host;
        int port;

        public Socket sock;

        StreamReader clientReader;
        StreamWriter clientWriter;

        public Client_server(String name, String host, int port, Socket sock)
        {
            this.name = name;
            this.host = host;
            this.port = port;
            this.sock = sock;

            this.clientReader = new StreamReader(new NetworkStream(sock));
            this.clientWriter = new StreamWriter(new NetworkStream(sock));
        }

        public void Send(string message)
        {
            clientWriter.WriteLine(message);
            clientWriter.Flush();
        }

        public string Receive()
        {
            string message;
            try
            {
                message = clientReader.ReadLine();
                return message;
            }
            catch
            {
                return null;
            }
        }
    }
}
