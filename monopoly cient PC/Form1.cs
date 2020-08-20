using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monopoly_cient_PC
{
	public partial class OwnBalance : Form
	{
		public OwnBalance(Connection connection)
		{
			InitializeComponent();
			this.Text = $"Monopoly";
			this.connection = connection;
			ownBalance = this;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			checkedListBox1.Items.Add("Jan");
			checkedListBox1.Items.Add("Paweł");
			button1.Enabled = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (checkedListBox1.CheckedItems.Count == 0)
			{
				ActionJsonObject jsonObject = new ActionJsonObject() { type = "pay", from = (int)id, howMany = (float)numericUpDown1.Value };
				connection.JsonSender(jsonObject);
			}
			else if (checkedListBox1.CheckedItems.Count == 1)
			{
				ActionJsonObject jsonObject = new ActionJsonObject() { type = "payTo", from = (int)id, to = ((User)checkedListBox1.CheckedItems[0]).id, howMany = (float)numericUpDown1.Value };
				connection.JsonSender(jsonObject);
			}
			else
			{
				textBox2.Text = "You selected too many players.";
			}
			checkedListBox1.ClearSelected();
		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (textBox1.TextLength >= 3)
			{
				SendJson(type: "newPlayer", message: textBox1.Text);
				button2.Enabled = false;
				textBox1.Enabled = false;
				username = textBox1.Text;
				button1.Enabled = true;
				Text = $"Monopoly - playing as {username}";
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			SendJson("dontAllowMorePlayers");
			// ActionJsonObject jsonObject = new ActionJsonObject() { type = "dontAllowMorePlayers", from = username };
			// connection.JsonSender(jsonObject);
			// button3.Enabled = false;
		}
		private void button4_Click(object sender, EventArgs e)
		{
			SendJson("addMoney", (int)id, (float)numericUpDown1.Value);
		}


		private void SendJson(string type, int to = 0, float howMany = 0, string message = null)
		{
			int tempid;
			if (id != null)
				tempid = (int)id;

			else
				tempid = 0;

			ActionJsonObject jsonObject = new ActionJsonObject() { type = type, from = tempid, to = (int)to, howMany = howMany, message = message };
			connection.JsonSender(jsonObject);
		}
		public string GetName()
		{
			return textBox1.Text;
		}

		readonly Connection connection;
		public static OwnBalance ownBalance;

		public string message
		{
			set { textBox2.Text = value; }
		}
		int? id = null;
		string username;

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
