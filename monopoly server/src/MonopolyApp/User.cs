using System;
using System.Collections.Generic;

namespace MonopolyApp
{
    class User
    {
     
     
     
     
        User(string name)
        {
            this.name = name;
            balance = 15;
            // if (UserCreatedEvent != null)
            // UserCreatedEvent(this, new TypeEventArgs(new ActionJsonObject()));
        }
        public override string ToString()
        {
            return name + ": " + balance.ToString() + "MLN";
        }
        public void PayFromTheBalance(double howMany)
        {
            if (howMany < 0)
                throw new ArgumentException($"You cannot pay the negative value. :/");
            if (howMany <= balance)
            {
                howMany = Math.Round(howMany, 2);
                balance -= howMany;
             //   if (PaidFromTheBalanceEvent != null)
             //   PaidFromTheBalanceEvent(this, new TypeEventArgs("a"));
            }
            else
                throw new ArgumentException($"{name} doesn't have enough money. Balance cannot be negative.");
        }
        public void PayToOtherPlayer(User otherPlayer, double howMany)
        {
            try
            {
                PayFromTheBalance(howMany);
             //   if (PaidToOtherPlayerEvent != null)
              //  PaidToOtherPlayerEvent(this, new TypeEventArgs("a"));
              //  otherPlayer.AddMoney(howMany);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        public void PassedStart()
        {
            AddMoney(2);
          // if (PassedStartEvent != null)
          // PassedStartEvent(this, new TypeEventArgs("a"));
        }
        public void AddMoney(double howMany)
        {
            if (howMany < 0)
                throw new ArgumentException($"You cannot get the negative value. :/");
            balance += howMany;
            Math.Round(balance, 2);
        //    if (AddedMoneyEvent != null)
          // AddedMoneyEvent(this, new TypeEventArgs("a"));
          // // Console.WriteLine($"\t{GetName()} + {howMany}MLN.");
        }
        public static User GetUser(List<User> listOfUsers, string username)
        {
            foreach (User i in listOfUsers)
            {
                if (i.name.ToLower() == username.ToLower())
                    return i;
            }
            throw new ArgumentException($"There is no user {username}.");
        }
        public static User CreateNewUser(List<User> listOfUsers, string username)
        {
            bool exists = false;
            foreach (User i in listOfUsers)
            {
                if (i.name.ToLower() == username.ToLower())
                    exists = true;
            }
            if (!exists)
                return new User(username);
            else
                throw new ArgumentException($"Name {username} is already used.");
        }
        public readonly string name;
        public double balance
        {
            get;
            private set;
        }
    }
}