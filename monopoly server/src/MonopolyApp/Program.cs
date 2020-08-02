using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MonopolyApp
{
    class Program
    {
        static void Main()
        {
            AddEvents.AddConsoleEvents();
            bool allowMorePlayers = true;
            ActionJsonObject operation;
            Queue<ActionJsonObject> json = new Queue<ActionJsonObject>();

            List<User> listOfUsers = new List<User>();
            Console.WriteLine(">> SERVER IS RUNNING.");

            Thread DataReader = new Thread(() => Connection.DataReader(ref json));
            DataReader.Name = "DataReader";
            DataReader.Start();        
            while (true)
            {
                if (json.Count > 0)
                {
                    operation = json.Dequeue();
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
