using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_MP_AP
{
    public class Client
    {
        public int Money { get; private set; }

        public Client()
        {
            Money = new Random().Next(75, 200);
        }

        public Client(int money)
        {
            Money = money;
        }
    }
}
