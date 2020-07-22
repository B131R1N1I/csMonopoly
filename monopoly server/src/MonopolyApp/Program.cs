using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;

namespace MonopolyApp
{
    class Program
    {
        static void Main()
        {
            AddEvents.AddConsoleEvents();
            
            bool allowMorePlayers = true;
            ActionJsonObject operation = new ActionJsonObject();
            List<User> listOfUsers = new List<User>();
            int PORT = 6666;
            IPAddress serverAddress = IPAddress.Any;
            TcpListener serverSocket = new TcpListener(serverAddress, PORT);
            serverSocket.Start();
            Console.WriteLine(">> SERVER IS RUNNING.");


            Byte[] bytes = new Byte[256];
            string data = null;

            while (true)
            {
                Console.WriteLine("Waiting for connection");

                TcpClient client = serverSocket.AcceptTcpClient();
                Console.WriteLine("Connected");

                data = null;

                NetworkStream stream = client.GetStream();
                int i;

                
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {

                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                }

                if (data != null)
                {
                    try
                    {
                        operation = JsonSerializer.Deserialize<ActionJsonObject>(data);
                    }
                    catch (JsonException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
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
