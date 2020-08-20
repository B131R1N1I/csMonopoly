using System;
using System.Collections.Generic;
namespace MonopolyApp
{
    public class SendObject
    {
        private ActionJsonObject actionJsonObject1;

        public string type { get; set; }
        public ActionJsonObject actionJsonObject { get => actionJsonObject1; set => actionJsonObject1 = value; }
        public string listOfUsers { get; set; }
    }
}