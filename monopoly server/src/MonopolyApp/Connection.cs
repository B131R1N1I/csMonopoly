using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace MonopolyApp
{
    class Connection
    {
        public static void DataReader(ref Queue<ActionJsonObject> json)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 6666);
            serverSocket.Start();
            System.Console.WriteLine("Ready to start Listening");
            Byte[] bytes = new byte[256];
            string data;

            
            while (true)
            {
                if (serverSocket.Pending())
                {
                    listOfStreams.AddLast(serverSocket.AcceptTcpClient().GetStream());
                }   
                foreach (NetworkStream stream in listOfStreams)
                {
                    
                    if (stream.DataAvailable)
                    {
                        System.Console.WriteLine(stream.DataAvailable);
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));
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
                // using (TcpClient connectedTcpClient = serverSocket.AcceptTcpClient())
                // {
                //     using (NetworkStream stream = connectedTcpClient.GetStream())
                //     {
                //         data = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));
                //         System.Console.WriteLine(data);
                //     }
                // }
            }
        }
        public static void DataSender(ActionJsonObject json)
        {
            Byte[] bytes = new byte[256];
            bytes = System.Text.Encoding.ASCII.GetBytes(JsonSerializer.Serialize<ActionJsonObject>(json));
            foreach (NetworkStream stream in listOfStreams)
            {
                System.Console.WriteLine("ERRRRR");
                stream.Write(bytes, 0, bytes.Length);
                System.Console.WriteLine($"Send {bytes.Length}");
            }
        }
        static LinkedList<NetworkStream> listOfStreams = new LinkedList<NetworkStream>();
        
    }
}