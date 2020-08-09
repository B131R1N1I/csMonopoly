using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Timers;

namespace MonopolyClientConsole
{
    class Program
    {
        static void Main()
        {
            Queue<ActionJsonObject> jsonRecived = new Queue<ActionJsonObject>();

            IPAddress serverAddress = IPAddress.Loopback;
            Connection serverConnection = new Connection(serverAddress);
            Thread dataReader = new Thread(() => serverConnection.DataReader(ref jsonRecived));
            dataReader.Start();


            string type, from, to, howManytemp;
            float howMany;

            while (true)
            {
                Console.Write("type: ");
                type = Console.ReadLine().Replace(" ", "");
                Console.Write("from: ");
                from = Console.ReadLine();
                Console.Write("to: ");
                to = Console.ReadLine();

                while (true)
                {
                    Console.Write("howMany: ");
                    howManytemp = Console.ReadLine();
                    if (float.TryParse(howManytemp, out howMany))
                    {
                        howMany = float.Parse(howManytemp);
                        break;
                    }
                    else if (howManytemp == "")
                        break;
                }
                serverConnection.JsonSender(new ActionJsonObject() { type = type, from = from, to = to, howMany = howMany });
                
                // // temporary
                // Thread.Sleep(1000);
                // if (json.Count > 0)
                //     // error message
                //     Console.WriteLine(json.Peek().from);
                //     serverConnection.JsonSender(json.Dequeue());
                    

            }
        }
    }
}
