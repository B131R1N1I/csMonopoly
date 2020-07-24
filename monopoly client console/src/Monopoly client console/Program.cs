using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;

namespace MonopolyClientConsole
{
    class Program
    {
        static void Main()
        {

            IPAddress serverAddress = IPAddress.Loopback;
            Connection ServerConnection = new Connection(serverAddress);


            string type, from, to, howManytemp;
            double howMany;

            while (true)
            {
                Console.Write("type: ");
                type = Console.ReadLine();
                Console.Write("from: ");
                from = Console.ReadLine();
                Console.Write("to: ");
                to = Console.ReadLine();

                while (true)
                {
                    Console.Write("howMany: ");
                    howManytemp = Console.ReadLine();
                    if (Double.TryParse(howManytemp, out howMany))
                    {
                        howMany = Double.Parse(howManytemp);
                        break;
                    }
                    else if (howManytemp == "")
                    break;
                }
                ActionJsonObject json = new ActionJsonObject() { type = type, from = from, to = to, howMany = howMany };
                ServerConnection.JsonSender(json);
            }
        }
    }
}
