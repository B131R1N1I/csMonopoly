using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    public class TypeEventArgs : EventArgs
    {
        public TypeEventArgs(ActionJsonObject actionJson, NetworkStream stream = null) : base()
        {
            this.actionJson = actionJson;
            this.stream = stream;
        }
        public readonly ActionJsonObject actionJson;
        public readonly NetworkStream stream;
    }
}