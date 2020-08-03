using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class ConsoleLogs
    {
        public static void ConsoleLogOnEvent(TypeEventArgs operation)
        {
            string toDo = operation.actionJson.type;
            string from = operation.actionJson.from;
            string to = operation.actionJson.to;
            double howMany = Math.Round(operation.actionJson.howMany, 2);

                switch (toDo)
                {
                    case "newPlayer":
                        Console.WriteLine($"Player {from} has been added.");
                        break;
                    case "dontAllowMorePlayers":
                        Console.WriteLine($"Now no one can join to this game. {from} has blocked it. " +
                                           "If you want more players to join - restart the system.");
                        break;
                    case "payTo":
                        Console.WriteLine($"{from} paid {howMany}MLN to {to}.");
                        break;
                    case "pay":
                        Console.WriteLine($"User {from} paid {howMany} MLN.");
                        break;
                    case "addMoney":
                        Console.WriteLine($"Added {howMany} MLN to {to}.");
                        break;
                    case "start":
                        Console.WriteLine($"{to} just passed the start and earn 2 MLN.");
                        break;
                    default:
                        Console.WriteLine("I don't recognize this command");
                        break;
                }
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