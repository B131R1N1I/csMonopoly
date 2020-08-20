using System;
using System.Collections.Generic;

namespace monopoly_cient_PC
{
	public class User
	{
		public override string ToString()
		{
			return $"{name} : {balance}";
		}
		public User(string name, float balance, int id)
		{
			this.name = name;
			this.balance = balance;
			this.id = id;
		}
		public string name
		{
			get;
		}
		public float balance
		{
			get;
			private set;
		}
		public int id { get; }
	}
}