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
            id = idcreator++;
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
            }
            else
                throw new ArgumentException($"{name} doesn't have enough money. Balance cannot be negative.");
        }
        public void PayToOtherPlayer(User otherPlayer, double howMany)
        {
            try
            {
                PayFromTheBalance(howMany);
                otherPlayer.AddMoney(howMany);
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
        public void PassedStart()
        {
            AddMoney(2);
        }
        public void AddMoney(double howMany)
        {
            if (howMany < 0)
                throw new ArgumentException($"You cannot get the negative value. :/");
            balance += howMany;
            Math.Round(balance, 2);
        }
        public static User GetUser(List<User> listOfUsers, string username)
        {

            foreach (User user in listOfUsers)
            {
                if (user.name.ToLower() == username.ToLower())
                    return user;
            }
            throw new ArgumentException($"There is no user {username}.");
        }
        public static User CreateNewUser(List<User> listOfUsers, string username)
        {
            if (listOfUsers.Find(user => user.name.ToLower() == username.ToLower()) != null)
                throw new ArgumentException($"Name {username} is already used.");

            return new User(username);
        }
        public readonly string name;
        public double balance
        {
            get;
            private set;
        }
        public readonly int id;
        static private int idcreator = 0;
    }
}