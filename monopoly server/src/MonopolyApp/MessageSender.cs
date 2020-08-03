using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class MessageSender
    {
        public static void SendErrorMessage(NetworkStream stream, Exception ex)
        {
            System.Console.WriteLine("error! " + ex.Message);
            ActionJsonObject a = new ActionJsonObject() {type = "Error", from = "wyskoczył"};
            Connection.DataSender(stream, a);
        }
    }
}