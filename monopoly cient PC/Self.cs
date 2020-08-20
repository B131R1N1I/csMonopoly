using System;
using System.Collections.Generic;
using System.Text;

namespace monopoly_cient_PC
{
	static class Self
	{ 
		static public string username
		{
			get => username;
			set {username = value; }
		}
		static public int? id
		{
			get => id;
			set { if (id == null) id = value; }
		}

	}
}
