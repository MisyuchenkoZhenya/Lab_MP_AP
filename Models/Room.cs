﻿using System;

namespace Lab_MP_AP
{
    public class Room
    {
        private Client _client;

        public DateTime CheckOutDate { get; private set; }
        public int Price { get; }
        public bool IsAvailabe
        {
            get { return _client == null; }
        }

        public Room(int price)
        {
            Price = price;
        }

        public void SettleClient(Client newClient, DateTime currentDateTime)
        {
            if (IsAvailabe)
            {
                _client = newClient;
                CheckOutDate = currentDateTime.AddDays(1);
            }
        }

        public void RemoveClient()
        {
            _client = null;
        }
    }
}
