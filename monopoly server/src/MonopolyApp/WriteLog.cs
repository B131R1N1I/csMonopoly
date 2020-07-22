using System;

namespace MonopolyApp
{
    delegate void WriteLog(TypeEventArgs args);
    delegate void WriteMessage(User[] args);
    delegate void WriteErrorMessage(Exception args);
}