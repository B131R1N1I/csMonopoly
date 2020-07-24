using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace MonopolyApp
{
    class Connection
    {
        public static void DataReader(ref Queue<ActionJsonObject> json, TcpListener serverSocket)
        {
            System.Console.WriteLine("th");
            Byte[] bytes = new byte[256];
            int i;
            string data;

            LinkedList<NetworkStream> listOfStreams = new LinkedList<NetworkStream>();
            while (true)
            {
                if (serverSocket.Pending())
                    listOfStreams.AddLast(serverSocket.AcceptTcpClient().GetStream());
                foreach (NetworkStream stream in listOfStreams)
                {
                    if (stream.DataAvailable)
                    {
                        i = stream.Read(bytes);
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        System.Console.WriteLine(data);
                        try
                        {
                            json.Enqueue(JsonSerializer.Deserialize<ActionJsonObject>(data));
                        }
                        catch (JsonException e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }
                }

            }
        }
    }
}