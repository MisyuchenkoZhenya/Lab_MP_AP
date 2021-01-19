using System;

namespace Lab_MP_AP
{
    public class Client
    {
        public int Money { get; private set; }
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
            Money = new Random().Next(75, 200);
        }

        public Client(string name, int money)
        {
            Name = name;
            Money = money;
        }
    }
}
