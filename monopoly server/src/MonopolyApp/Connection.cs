using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
// using System.Text.Json;
using System.Threading;
using Newtonsoft.Json;

namespace MonopolyApp
{
    class Connection
    {
        static public event WriteErrorMessage CannotDeserializeDataEvent;
        public static void DataReader(ref Queue<StreamWithAction> json)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 6666);
            serverSocket.Start();
            System.Console.WriteLine("Ready to start Listening");
            Byte[] bytes = new byte[256];
            string data;
            NetworkStream stream;


            while (true)
            {
                if (serverSocket.Pending())
                {
                    listOfStreams.Add(serverSocket.AcceptTcpClient().GetStream());
                }
                // foreach (NetworkStream stream in listOfStreams)
                // {

                //     if (stream.DataAvailable)
                //     {
                //         System.Console.WriteLine(stream.DataAvailable);
                //         data = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));
                //         System.Console.WriteLine(data);
                //         try
                //         {
                //             json.Enqueue(JsonSerializer.Deserialize<ActionJsonObject>(data));
                //         }
                //         catch (JsonException e)
                //         {
                //             System.Console.WriteLine(e.Message);
                //         }
                //     }
                // }
                if (listOfStreams.Find(s => s.DataAvailable == true) != null)
                {
                    stream = listOfStreams.Find(s => s.DataAvailable == true);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));
                    System.Console.WriteLine(data);
                    try
                    {
                        json.Enqueue(new StreamWithAction(JsonConvert.DeserializeObject<ActionJsonObject>(data), stream));
                    }
                    catch (JsonException e)
                    {
                        if (CannotDeserializeDataEvent != null)
                            CannotDeserializeDataEvent(stream, e);
                    }
                }
                //         try
                //         {
                //             json.Enqueue(JsonSerializer.Deserialize<ActionJsonObject>(data));
                //         }
                //         catch (JsonException e)
                //         {
                //             System.Console.WriteLine(e.Message);
                //         }

                // using (TcpClient connectedTcpClient = serverSocket.AcceptTcpClient())
                // {
                //     using (NetworkStream stream = connectedTcpClient.GetStream())
                //     {
                //         data = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));
                //         System.Console.WriteLine(data);
                //     }
                // }
            }
        }
        public static void DataSender(NetworkStream stream, ActionJsonObject json)
        {
            if (stream == null)
                DataSender(listOfStreams.ToArray(), json);
            //Byte[] bytes = new byte[256];
            SendObject sendObject = new SendObject() { type = "operation", actionJsonObject = json };
            Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(sendObject));

            if (stream != null)
            {
                System.Console.WriteLine("WRITE");
                stream.Write(bytes, 0, bytes.Length);

                System.Console.WriteLine($"Send {bytes.Length}");
            }

        }
        public static void DataSender(NetworkStream[] streams, ActionJsonObject json)
        {
            foreach (NetworkStream stream in streams)
                DataSender(stream, json);
        }
        public static void BalanceSender(User[] listOfUsers)
        {
            Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(new SendObject() 
            { type = "users", listOfUsers = JsonConvert.SerializeObject(listOfUsers)}));
            System.Console.WriteLine(JsonConvert.SerializeObject(new SendObject() 
            { type = "users", listOfUsers = JsonConvert.SerializeObject(listOfUsers)}));
            foreach (User item in listOfUsers)
            {
                System.Console.WriteLine(item);
            }
            foreach (NetworkStream stream in listOfStreams.ToArray())
            {
                stream.Write(bytes, 0, bytes.Length);
                
                System.Console.WriteLine($"Sent {bytes.Length}");
            }
        }
        // public static void DataSender(ActionJsonObject json)
        // {
        //     Byte[] bytes = new byte[256];
        //     bytes = System.Text.Encoding.ASCII.GetBytes(JsonSerializer.Serialize<ActionJsonObject>(json));
        //     foreach (NetworkStream stream in listOfStreams)
        //     {
        //         System.Console.WriteLine("ERRRRR");
        //         stream.Write(bytes, 0, bytes.Length);
        //         System.Console.WriteLine($"Send {bytes.Length}");
        //     }
        // }
        static List<NetworkStream> listOfStreams = new List<NetworkStream>();

    }
}