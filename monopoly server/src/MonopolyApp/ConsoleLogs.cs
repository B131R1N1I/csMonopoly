using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class ConsoleLogs
    {
        public static void ConsoleLogOnEvent(TypeEventArgs operation)
        {
            Console.WriteLine(Cases.ActionMessage(operation.actionJson));
        }

        public static void ConsoleLogOnEvent(User[] args)
        {
            foreach (User user in args)
                Console.WriteLine(user);
        }

        public static void ConsoleLogOnEvent(NetworkStream x, Exception args)
        {
            Console.WriteLine(args.Message);
        }

    }
}