using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace monopoly_cient_PC
{
	class UserStatus
	{
		public static void UserUpdater(User[] UpdatedListOfUsers)
		{
			listOfUsers = UpdatedListOfUsers.ToList<User>();
		}
		static private List<User> listOfUsers;
		
	}
}