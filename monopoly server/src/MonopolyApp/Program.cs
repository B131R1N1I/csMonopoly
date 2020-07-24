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
            
            ActionJsonObject operation = new ActionJsonObject();
            Queue<ActionJsonObject> json = new Queue<ActionJsonObject>();

            List<User> listOfUsers = new List<User>();
            int PORT = 6666;
            IPAddress serverAddress = IPAddress.Any;
            TcpListener serverSocket = new TcpListener(serverAddress, PORT);
            serverSocket.Start();
            Console.WriteLine(">> SERVER IS RUNNING.");

            Thread DataReader = new Thread(() => Connection.DataReader(ref json, serverSocket));
            DataReader.Start();        
            while (true)
            {
                if (json.Count > 0)
                {
                    operation = json.Dequeue();
                    Console.WriteLine("----------");
                    Console.WriteLine("===============");
                    try
                    {
                        Cases.Action(operation, ref listOfUsers);
                    }
                    catch (Exception){}
                    Console.WriteLine("===============");

                }

            }

        }
    }
}
