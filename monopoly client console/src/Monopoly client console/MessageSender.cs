using System;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;

namespace MonopolyClientConsole
{
    class Connection
    {
        public Connection(IPAddress serverAddress)
        {
            this.serverAddress = serverAddress;
            clientSocket.Connect(serverAddress, PORT);
        }
        public void JsonSender(ActionJsonObject json)
        {
            Byte[] toSend = new Byte[256];
            string jsonString = JsonSerializer.Serialize(json);
            toSend = System.Text.Encoding.ASCII.GetBytes(jsonString);

            Console.WriteLine(jsonString);
            
            NetworkStream stream = clientSocket.GetStream();
            stream.Write(toSend, 0, toSend.Length);
            System.Console.WriteLine("SEND");

        }
        TcpClient clientSocket = new TcpClient();
        IPAddress serverAddress;
        const int PORT = 6666;
    }
}