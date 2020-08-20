using System;

namespace MonopolyApp
{
    partial class Cases
    {
        public static string ActionMessage(ActionJsonObject json)
        {
            string toDo = json.type;
            int from = json.from;
            int to = json.to;
            double howMany = Math.Round(json.howMany, 2);

                switch (toDo)
                {
                    case "newPlayer":
                        return $"Player {json.message} has been added.";

                    case "dontAllowMorePlayers":
                        return $"Now no one can join to this game. {from} has blocked it. " +
                                           "If you want more players to join - restart the system.";

                    case "payTo":
                        return $"{from} paid {howMany}MLN to {to}.";

                    case "pay":
                        return $"User {from} paid {howMany} MLN.";

                    case "addMoney":
                        return $"Added {howMany} MLN to {to}.";

                    case "start":
                        return $"{to} just passed the start and earn 2 MLN.";

                    default:
                        return "I don't recognize this command";

                }
        }
    }
}