using System;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace monopoly_cient_PC
{
	public partial class Connection
	{
		public Connection(IPAddress serverAddress)
		{
			this.serverAddress = serverAddress;
			clientSocket = ConnectToServer();
		}
		public void JsonSender(ActionJsonObject json)
		{
			string jsonString = JsonConvert.SerializeObject(json);
			byte[] toSend = System.Text.Encoding.ASCII.GetBytes(jsonString);


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
		public void DataReader(ref Queue<SendObject> json)
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

						jsonString = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes));


						try
						{
							json.Enqueue(JsonConvert.DeserializeObject<SendObject>(jsonString));

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
				

			}
		}
		public void Updater(ref Queue<SendObject> json)
		{
			while (true)
			{
				if (json.Count > 0)
				{
					// OwnBalance.ownBalance.textBox2.Text = json.Dequeue().from;
					// if (json.Count > 0 & json.Peek().type == "Playerers")
					//   UserStatus.UserUpdater(json.Dequeue());
					// else
					if (json.Peek().type == "operation")
						OwnBalance.ownBalance.SetMessage(json.Dequeue().actionJsonObject.message);
					else if (json.Peek().type == "users")
					{
						bool isFirst = true;
						foreach (User user in JsonConvert.DeserializeObject<User[]>(json.Dequeue().listOfUsers))
						{
							
							OwnBalance.ownBalance.UpdateUser(user, isFirst);
							isFirst = false;
						}
					}
				}
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
