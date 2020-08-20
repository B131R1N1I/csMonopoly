using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class MessageSender
    {
        public static void SendErrorMessage(NetworkStream stream, Exception ex)
        {
            System.Console.WriteLine("error! " + ex.Message);
            ActionJsonObject a = new ActionJsonObject() {type = "Error", message = ex.Message};
            Connection.DataSender(stream, a);
        }
        public static void SendMessage(TypeEventArgs streamWithAction)
        {
            System.Console.WriteLine("Success message: " + Cases.ActionMessage(streamWithAction.actionJson));
            ActionJsonObject a = new ActionJsonObject() {type = "Message", message = Cases.ActionMessage(streamWithAction.actionJson)};
            Connection.DataSender(streamWithAction.stream, a);
        }
    }
}