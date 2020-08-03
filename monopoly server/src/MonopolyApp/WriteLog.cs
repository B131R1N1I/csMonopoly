using System;
using System.Net.Sockets;

namespace MonopolyApp
{
    delegate void WriteLog(TypeEventArgs args);
    delegate void WriteMessage(User[] args);
    delegate void WriteErrorMessage(NetworkStream stream, Exception args);

}