using System;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;


namespace MonopolyClientConsole
{
    class Connection
    {
        public Connection(IPAddress serverAddress)
        {
            this.serverAddress = serverAddress;
            clientSocket.Connect(serverAddress, PORT);
        }
        public void JsonSender(ActionJsonObject json)
        {
            Byte[] toSend = new Byte[256];
            string jsonString = JsonSerializer.Serialize(json);
            toSend = System.Text.Encoding.ASCII.GetBytes(jsonString);

            Console.WriteLine(jsonString);

            NetworkStream stream = clientSocket.GetStream();
            try
            {
                if (!stream.CanWrite)
                    throw new System.IO.IOException("CAnt read");
                stream.Write(toSend, 0, toSend.Length);
                System.Console.WriteLine("SEND");
            }
            catch (System.IO.IOException ex)
            {
                System.Console.WriteLine(ex.Message);

                clientSocket.Close();
                clientSocket = new TcpClient();
                clientSocket.Connect(serverAddress, PORT);
                try
                {
                    JsonSender(json);
                    System.Console.WriteLine("Connection fixed.");
                }
                catch (Exception e)
                {

                    System.Console.WriteLine(e.Message);
                    System.Console.WriteLine("Cannot send message. Maybe sever is down.");
                }



            }


        }
        public void DataReader(ref Queue<ActionJsonObject> json)
        {
            Byte[] bytes = new byte[256];
            string jsonString;
            NetworkStream stream = clientSocket.GetStream();
            while (true)
            {
                try
                {
                    if (stream.DataAvailable)
                    {
                        System.Console.WriteLine("a");


                        jsonString = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));


                        try
                        {
                            System.Console.WriteLine(jsonString);
                            json.Enqueue(JsonSerializer.Deserialize<ActionJsonObject>(jsonString));
                            System.Console.WriteLine(json.Count);
                            Console.Beep();
                        }
                        catch (JsonException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                catch (ObjectDisposedException ex)
                {
                    System.Console.WriteLine("!!!!!!" + ex.Message);
                    Console.Beep();
                    continue;
                }
            }
        }
        TcpClient clientSocket = new TcpClient();
        IPAddress serverAddress;
        const int PORT = 6666;
    }
}