using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Threading;

namespace MonopolyApp
{
    class Program
    {
        static void Main()
        {
            AddEvents.AddConsoleEvents();
            
            bool allowMorePlayers = true;
            ActionJsonObject operation = new ActionJsonObject();
            Queue<ActionJsonObject> json = new Queue<ActionJsonObject>();

            List<User> listOfUsers = new List<User>();
            int PORT = 6666;
            IPAddress serverAddress = IPAddress.Any;
            TcpListener serverSocket = new TcpListener(serverAddress, PORT);
            serverSocket.Start();
            Console.WriteLine(">> SERVER IS RUNNING.");

            Thread th = new Thread(() => Connection.DataReader(ref json, serverSocket));
            th.Start();
            Byte[] bytes = new Byte[256];
            // string data;
            
            while (true)
            {
                // Console.WriteLine("Waiting for connection");

                // TcpClient client = serverSocket.AcceptTcpClient();
                // Console.WriteLine("Connected");

                // data = null;

                // NetworkStream stream = client.GetStream();
                //     try
                //     {
                //     int i = stream.Read(bytes);
                    
                //     data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                //     }
                //     catch (System.IO.IOException ex)
                //     {
                //         System.Console.WriteLine(ex.Message);
                //         continue;
                //     }
                    
                //     System.Console.WriteLine(data);

                if (json.Count > 0)
                {
                    // try
                    // {
                    //     operation = JsonSerializer.Deserialize<ActionJsonObject>(data);
                    // }
                    // catch (JsonException e)
                    // {
                    //     System.Console.WriteLine("json exec");
                    //     Console.WriteLine(e.Message);
                    //     continue;
                    // }
                    operation = json.Dequeue();
                    Console.WriteLine("----------");
                    Console.WriteLine(operation);
                    Console.WriteLine("===============");
                    try
                    {
                        Cases.Action(operation, ref listOfUsers, ref allowMorePlayers);
                    }
                    catch (Exception){}
                    Console.WriteLine("===============");

                }

            }

        }
    }
}
