using System;
using System.Text.Json;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;


namespace MonopolyClientConsole
{
    class Connection
    {
        public Connection(IPAddress serverAddress)
        {
            this.serverAddress = serverAddress;
            clientSocket = ConnectToServer();
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
                if (clientSocket.Client.Connected)
                {
                    if (!stream.CanWrite)
                        throw new System.IO.IOException("CAnt write");
                    stream.Write(toSend, 0, toSend.Length);
                    System.Console.WriteLine("SEND");
                }
                else
                    throw new Exception();
            }
            catch (System.IO.IOException ex)
            {
                System.Console.WriteLine(ex.Message);

                try
                {
                    JsonSender(json);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    clientSocket = ConnectToServer();
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

                    if (clientSocket.Connected & stream.DataAvailable)
                    {
                        System.Console.WriteLine("stream.DataAvailable == true");


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
                    System.Console.WriteLine($"!!!!!!{ex.Message}");
                    Console.Beep();
                    continue;
                }
                if (json.Count > 0)
                    Console.WriteLine($"------{json.Dequeue().from}");
            }
        }
        TcpClient ConnectToServer()
        {
            clientSocket = new TcpClient();
            while (!clientSocket.Connected)
            {
                try
                {
                    clientSocket.Connect(serverAddress, PORT);
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
            return clientSocket;

        }
        TcpClient clientSocket = new TcpClient();
        IPAddress serverAddress;
        const int PORT = 6666;
    }
}