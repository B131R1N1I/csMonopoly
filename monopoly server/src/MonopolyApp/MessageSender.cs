using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class MessageSender
    {
        public static void SendErrorMessage(Exception ex)
        {
            System.Console.WriteLine("error!");
            ActionJsonObject a = new ActionJsonObject() {type = "Error", from = "wyskoczył"};
            Connection.DataSender(a);
        }
    }
}