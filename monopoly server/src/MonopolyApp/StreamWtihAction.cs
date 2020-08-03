using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    class StreamWithAction
    {
        public StreamWithAction(ActionJsonObject jsonObject, NetworkStream stream)
        {
            this.jsonObject = jsonObject;
            this.stream = stream;
        }
        public readonly ActionJsonObject jsonObject;
        public readonly NetworkStream stream;
    }
}