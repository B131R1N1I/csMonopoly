using System;
using System.Collections.Generic;
using System.Text;

// own methods
namespace monopoly_cient_PC
{
	//delegate void AccessToSetMessageFromOtherThread(string text);
	//delegate void AccessToUpdateUserFromOtherThread(User[] users);
	public partial class OwnBalance
	{
		public void SetMessage(string message)
		{
			if (this.textBox2.InvokeRequired)
			{
				this.Invoke(new Action<string>(SetMessage), message);
				return;
			}
			this.textBox2.Text = message;
		}
		public void UpdateUser(User user, bool isFirst)
		{
			if (this.checkedListBox1.InvokeRequired)
			{

				this.Invoke(new Action<User, bool>(UpdateUser), user, isFirst);
				return;
			}
			if (isFirst)
				this.checkedListBox1.Items.Clear();
			if (id == null & user.name == OwnBalance.ownBalance.textBox1.Text)
				id = user.id;

				this.checkedListBox1.Items.Add(user);


		}
	}
}
