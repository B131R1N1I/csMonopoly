using System;

namespace monopoly_cient_PC
{
	public class ActionJsonObject
	{
		public override string ToString()
		{
			return $"type: {type}\nfrom: {from}\nto: {to}\nhowMany: {howMany}\nmessage: {message}";
		}
		public string type { get; set; }
		public int from { get; set; }
		public int to { get; set; }
		public float howMany { get; set; }
		public string message { get; set; }
	}
}