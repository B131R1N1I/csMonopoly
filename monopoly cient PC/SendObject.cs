using System;
using System.Collections.Generic;
namespace monopoly_cient_PC
{
	public class SendObject
	{
		//   public override string ToString()
		//   {
		//       string lou = "";
		//       foreach (var item in listOfUsers)
		//       {
		//           lou += item + " ";
		//       }
		//       return $"{type} \n==!!!==\n {actionJsonObject} \n==!!!== {lou}";
		//   }
		public string type { get; set; }
		public ActionJsonObject actionJsonObject { get; set; }
		public string listOfUsers { get; set; }
	}
}